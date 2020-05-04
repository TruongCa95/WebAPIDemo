using System.Threading.Tasks;
using Data;

namespace Repositories.Interface
{
   public interface IUserRepository
   {
       Task<Users> FindMemberByEmailAndPassword(string email, string password);
       bool CheckExitMember(string name);
   }
}
