using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Raika.Common.SharedApplicationServices.Behaviours;
using System.Reflection;

namespace Raika.HomeAlarmPanel.Application
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
