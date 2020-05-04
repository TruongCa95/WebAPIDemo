using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories.Implement;
using Repositories.Interface;

namespace Services
{
   public class UserService : IUserService
   {
       private readonly IUnitOfWork _unitOfWork;
       private readonly Appsetting _appsetting;
       private APIDataContext _dbContext; 
       public UserService(APIDataContext dbContext , IUnitOfWork unitOfWork, IOptions<Appsetting> appsetting)
       {
           _unitOfWork = unitOfWork;
           _appsetting = appsetting.Value;
           _dbContext = dbContext;
       }
        public bool IsUserExits(string name)
        {
            return _unitOfWork.UserRepository.CheckExitMember(name);
        }

        public async Task<Users> GetListUser(int id)
        {
            var userList = await _unitOfWork.UserRepository.GetById(id);
            userList.Password = null;
            return userList;
        }

        public async Task<Users> AddUserAsync(Users users)
        {
           _unitOfWork.UserRepository.Add(users);
           await _unitOfWork.SaveChanges();
           return users;
        }

        public IList<Users> GetUsers()
        {
            return _dbContext.Users.ToList();
        }
        public async Task<Users> UpdateUserAsync(Users users)
        {
            var currentUser = await _unitOfWork.UserRepository.FindMemberByEmailAndPassword(users.Email, users.Password);
            currentUser.UserName = users.UserName;
            currentUser.PhoneNumber = users.PhoneNumber;
            currentUser.Gender = users.Gender;
            currentUser.DateOfBirth = users.DateOfBirth;
            currentUser.EmailOptIn = users.EmailOptIn;
            await _unitOfWork.SaveChanges();
            return currentUser;

        }

        public async Task<Users> UserAuthentication(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.FindMemberByEmailAndPassword(email, password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }

        public async Task<Users> DeleteUserAsync(Users users)
        {
            var currentUser = await _unitOfWork.UserRepository.FindMemberByEmailAndPassword(users.Email, users.Password);
            _unitOfWork.UserRepository.Delete(currentUser);
            await _unitOfWork.SaveChanges();
            return users;

        }
    }
}
