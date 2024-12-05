using Microsoft.Extensions.DependencyInjection;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Infrastructure.Repositories.QueryRepositories;

namespace Raika.HomeAlarmPanel.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //
            // Db Contexts
            //
            services.AddScoped<CommandDbContext>();
            services.AddScoped<QueryDbContext>();

            //
            // Commands
            //
            services.AddScoped<IAuditLogCommandRepository, AuditLogCommandRepository>();

            //
            // Queries
            //            
            services.AddScoped<IAuditLogQueryRepository, AuditLogQueryRepository>();

            return services;
        }
    }
}
