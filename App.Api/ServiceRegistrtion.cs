using App.Api.Converter;
using App.Api.Services;
using App.Application;
using App.Application.Behaviours;
using App.Application.Interfaces;
using App.Domain.Common;
using App.Persistence;

namespace App.Api
{
    internal static class ServiceRegistrtion
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IMailService, EmailService>();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new FluentErrorValidationAttribute());
            }).AddJsonOptions(option =>
            {
                // adding datetime and timeonly json converte
                // not supported by default.
                option.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                option.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
            var mailConfig = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailConfig);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
