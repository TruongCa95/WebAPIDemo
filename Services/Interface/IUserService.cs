using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Services
{
   public interface IUserService
   {
       bool IsUserExits(string name);
       Task<Users> GetListUser(int id);
       Task<Users> AddUserAsync(Users users);
       IList<Users> GetUsers();
       Task<Users> UpdateUserAsync(Users users);
       Task<Users> UserAuthentication(string email, string password);
   }
}
