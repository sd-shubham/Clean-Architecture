using App.Application.Attributes;
using App.Application.Interfaces;
using App.Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repositories
{
    [Injectable]
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<IReadOnlyCollection<User>> GetAsync(CancellationToken cancellationToken)
        {
            var Users = await _dbContext.Users.Select(x => new User
            {
                Id = x.Id,
                UserName = x.UserName
            }).AsNoTracking().ToListAsync(cancellationToken);
            return Users;

        }
    }
}
