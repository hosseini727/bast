using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.RepositoryBase;

namespace Raika.HomeAlarmPanel.Infrastructure.Repositories.QueryRepositories
{
    public class AuditLogQueryRepository : QueryRepositoryBase<AuditLog, Guid>, IAuditLogQueryRepository
    {
        private readonly QueryDbContext _queryDbContext;
        public AuditLogQueryRepository(QueryDbContext queryDbContext) : base(queryDbContext) => _queryDbContext = queryDbContext;
    }
}
