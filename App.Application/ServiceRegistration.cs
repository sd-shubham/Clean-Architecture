using App.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using App.Application.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using FluentValidation;

namespace App.Application
{
   public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection service, IConfiguration config )
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());
            //service.AddFluentValidation(x =>
            //{
            //    x.AutomaticValidationEnabled = true;
            //    x.RegisterValidatorsFromAssemblyContaining<ApplicationRegister>();
            //});
            //service.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            service.AddScoped(typeof(IPipelineBehavior<,>), typeof(AppLoggingBehaviour<,>));
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(AppValidationBehaviour<,>));

            //  Azure AD setup
            service.AddMicrosoftIdentityWebAppAuthentication(config);
            service.AddAuthentication();
            service.AddAuthorization();
        }
    }
}
