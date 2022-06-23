using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task<int> Complete(CancellationToken token = default);

    }
}
