using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.Domain.Entities;

namespace Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories
{
    public interface IAuditLogQueryRepository : IQueryGenericRepository<AuditLog, Guid>
    {
    }
}
