using App.Application.Interfaces;
using App.Domain.Enities;
using App.Persistence.Converter;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Persistence
{
    internal class AppDbContext: DbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions<AppDbContext> options,
                                     IDateTime dateTime, ICurrentUserService currentUserService):base(options)
        {
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        public DbSet<User> Users { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedBy = _currentUserService.UserId; 
                        entity.Entity.ModifiedBy = _currentUserService.UserId;
                        entity.Entity.CreatedOn = _dateTime.Now;
                        entity.Entity.ModifiedOn = _dateTime.Now;
                        break;
                        case EntityState.Modified:
                        entity.Entity.ModifiedBy = _currentUserService.UserId;
                        entity.Entity.ModifiedOn = _dateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(200)");
            configurationBuilder.Properties<decimal>().HavePrecision(14,2);
            configurationBuilder.Properties<DateOnly>()
                                 .HaveConversion<DateOnlyConverter>()
                                 .HaveColumnType("date");
            configurationBuilder.Properties<TimeOnly>()
                                .HaveConversion<TimeOnlyConverter>()
                                .HaveColumnType("time");
            base.ConfigureConventions(configurationBuilder);
        }
    }
}