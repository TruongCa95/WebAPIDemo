using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly APIDataContext _dbContext;

        public UserRepository(APIDataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Users> FindMemberByEmailAndPassword(string email, string password)
        {
            try
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                throw;
            }
        }

        public bool CheckExitMember(string name)
        {
            return _dbContext.Users.Any(x => x.UserName == name);
        }
    }
}
