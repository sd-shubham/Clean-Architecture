using App.Application.Interfaces;
using App.Domain;
using App.Persistence.Repositories;
using App.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrutor;
using System.Reflection;

namespace App.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemory")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
            });
            //services.AddDbContext<AppDbContext>(option =>

            //    option.UseSqlServer(
            //         configuration.GetConnectionString("AppDbContext"),
            //         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            //         ).LogTo(Console.WriteLine, LogLevel.Debug)
            //          .EnableSensitiveDataLogging()
            //         );
            //services.AddDbContext<ForexTradingDbContext>(options => {
            //    options.UseNpgsql("your connect string here");
            //});
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            var assembly = Assembly.LoadFrom(
                $"{Path.GetDirectoryName(typeof(AppDbContext).Assembly.Location)}\\App.Persistence.dll");
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(classes => classes.WithAttribute<Injectable>(), true)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    );

        }
    }
}