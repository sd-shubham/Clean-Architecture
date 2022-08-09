using App.Api.Converter;
using App.Api.Services;
using App.Application;
using App.Application.Behaviours;
using App.Application.Interfaces;
using App.Domain.Common;
using App.Persistence;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Identity.Web;

namespace App.Api
{
    internal static class ServiceRegistration
    {
        internal static void AddServices(this WebApplicationBuilder builder )
        {

            // key vault set up

            string kvUrl = builder.Configuration["KeyVaultConfig:KVUrl"];
            string tenantId = builder.Configuration["KeyVaultConfig:TenantId"];
            string clientId = builder.Configuration["KeyVaultConfig:ClientId"];
            string clientSecret = "W1t8Q~msDJuAh9UTJjr8ntLnv7PEQR369zxLJaYZ";

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var client = new SecretClient(new Uri(kvUrl), credential);
            builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

            // Add services to the container.
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IMailService, EmailService>();
            builder.Services.AddControllers(options =>
            {
              // options.Filters.Add(new FluentErrorValidationAttribute());
            }).AddJsonOptions(option =>
            {
                // adding datetime and timeonly json converte
                // not supported by default.
                option.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                option.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
            var mailConfig = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailConfig);

            builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
