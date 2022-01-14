using System.Linq.Expressions;

namespace App.Application.Interfaces
{
    public interface IBaseRepository<Entity>: IDisposable where Entity: class
    {
        // sync method
        public void Add(Entity entity);
        public void Update(Entity entity);
        public int SaveChanges();

        //Async
        Task<IReadOnlyCollection<Entity>> GetAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Entity entity);
        Task<Entity> FirstOrDefaultAsync(Expression<Func<Entity, bool>> expression, CancellationToken cancellationToken = default);
        Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> expression,CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<Entity, bool>> expression,CancellationToken cancellationToken= default);
        Task<int> SaveChangesAsync(CancellationToken token = default);

    }
}
