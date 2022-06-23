using App.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
#nullable disable
namespace App.Persistence.Repositories
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Entity> EntitySet => _dbContext.Set<Entity>();

        public BaseRepository(AppDbContext dbContext) => _dbContext = dbContext;


        public void Add(Entity entity) => EntitySet.Add(entity);
        public void Update(Entity entity) => EntitySet.Update(entity);
        public int SaveChanges() => _dbContext.SaveChanges();
        public virtual async Task<IReadOnlyCollection<Entity>> GetAsync(CancellationToken cancellationToken)

              => await EntitySet.AsNoTracking().ToListAsync(cancellationToken);
        public async Task AddAsync(Entity entity) => await EntitySet.AddAsync(entity);
        public async Task<Entity> FirstOrDefaultAsync(Expression<Func<Entity, bool>> expression, CancellationToken cancellationToken)
       
            => await EntitySet.FirstOrDefaultAsync(expression, cancellationToken);
      
        public async Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> expression, CancellationToken cancellationToken)

            => await EntitySet.SingleOrDefaultAsync(expression, cancellationToken);

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> expression, CancellationToken cancellationToken)

          => await EntitySet.AnyAsync(expression, cancellationToken);

        //public async Task<int> SaveChangesAsync(CancellationToken token = new CancellationToken())
        //    => await _dbContext.SaveChangesAsync(token);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
