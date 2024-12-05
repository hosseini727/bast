using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.Domain.Entities;

namespace Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories
{
    public interface IAuditLogCommandRepository : ICommandGenericRepository<AuditLog>
    {
    }
}
