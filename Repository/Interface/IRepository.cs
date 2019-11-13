using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interface
{
  public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
