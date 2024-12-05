using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.RepositoryBase;

namespace Raika.HomeAlarmPanel.Infrastructure.Repositories.CommandRepositories
{
    public class AuditLogCommandRepository : CommandRepositoryBase<AuditLog>, IAuditLogCommandRepository
    {
        private readonly CommandDbContext _commandDbContext;
        public AuditLogCommandRepository(CommandDbContext commandDbContext) : base(commandDbContext)
                => commandDbContext = _commandDbContext;
    }
}
