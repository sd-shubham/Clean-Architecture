using App.Application.Interfaces;
using App.Domain.Common;
using App.Domain.Enities;
using App.Persistence.Converter;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;
        public AppDbContext(
                             DbContextOptions<AppDbContext> options,
                             IDateTime dateTime,
                             ICurrentUserService currentUserService,
                             IDomainEventService domainEventService
            ) : base(options)
        {
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Account { get; set; }
        public  DbSet<UserAddress>Address => Set<UserAddress>();
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
            var events = ChangeTracker.Entries<IHasDomainEvent>()
                                      .Select(x => x.Entity.DomainEvents)
                                      .SelectMany(x => x)
                                      .Where(de => !de.IsPublished)
                                      .ToArray();
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvent(events);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // default config for all entity
            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(200)");
            configurationBuilder.Properties<decimal>().HavePrecision(14, 2);
            configurationBuilder.Properties<DateOnly>()
                                 .HaveConversion<DateOnlyConverter>()
                                 .HaveColumnType("date");
            configurationBuilder.Properties<TimeOnly>()
                                .HaveConversion<TimeOnlyConverter>()
                                .HaveColumnType("time");
            base.ConfigureConventions(configurationBuilder);
        }
        private async Task DispatchEvent(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}