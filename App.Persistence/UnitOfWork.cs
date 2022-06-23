using App.Application.Interfaces;
using App.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
        }
        public IUserRepository UserRepository { get; }

        public async Task<int> Complete(CancellationToken token = default)
        {
            return await _dbContext.SaveChangesAsync(token);
        }
    }
}
