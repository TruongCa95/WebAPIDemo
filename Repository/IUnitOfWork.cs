using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys
{
   public interface IUnitOfWork: IDisposable
   {
       IRepository<T> GetRepository<T>() where T : class;
       void Save();
       void Dispose();
   }
}
