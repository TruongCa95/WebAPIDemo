using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Services
{
   public interface IUserService
   {
       bool IsUserExits(string name);
       Task<Users> GetListUser(string id);
       Task<Users> AddUserAsync(Users users);

       Task UpdateUserAsync(Users users);
       Task<Users> UserAuthentication(string email, string password);
   }
}
