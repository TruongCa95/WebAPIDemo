using System;
using System.Threading.Tasks;
using Repositories;
using Repositories.Implement;

namespace Repositories.Interface
{
   public interface IUnitOfWork: IDisposable
   {
       Task<int> SaveChanges();
       UserRepository UserRepository { get; }
   }
}
