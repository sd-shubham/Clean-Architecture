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
        public UnitOfWork(AppDbContext dbContext)
        {
            UserRepository = new UserRepository(dbContext);
        }
        public IUserRepository UserRepository { get; }
    }
}
