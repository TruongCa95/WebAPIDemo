using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Data;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDataContext _dbContext;
        private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_dbContext));
            }
        }

        public UnitOfWork(APIDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
           _dbContext.Dispose();
        }

        public async Task<int> SaveChanges()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
