using App.Application.Interfaces;
using App.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace App.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // for testing -using inmemmory
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseInMemoryDatabase("InMemory")
            //    .LogTo(Console.WriteLine,LogLevel.Information)
            //    .EnableSensitiveDataLogging();
            //});
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
            });
            //services.AddDbContext<ForexTradingDbContext>(options => {
            //    options.UseNpgsql("your connect string here");
            //});
            services.AddTransient<IDateTime, DateTimeService>();
            // register all repos dynamically.
            services.Scan(x =>
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                var referencedAssembly = entryAssembly?.GetReferencedAssemblies().Select(Assembly.Load)!;
                var assemblies = new List<Assembly> { entryAssembly! }.Concat(referencedAssembly);
                x.FromAssemblies(assemblies).AddClasses(
                    classes => classes.AssignableTo(typeof(IBaseRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            });
        }
    }
}