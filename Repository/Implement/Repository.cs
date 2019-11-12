using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly APIDataContext _dbContext;

        public Repository(APIDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public async Task<T> GetById(string id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
             _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
