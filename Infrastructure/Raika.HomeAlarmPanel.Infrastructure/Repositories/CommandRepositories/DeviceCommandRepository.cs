using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.RepositoryBase;
using Raika.HomeAlarmPanel.Domain.Entities;


namespace Raika.HomeAlarmPanel.Infrastructure.Repositories.CommandRepositories
{
    public class DeviceCommandRepository : CommandRepositoryBase<Device>, IDeviceCommandRepository
    {
        private readonly CommandDbContext _commandDbContext;
        public DeviceCommandRepository(CommandDbContext commandDbContext) : base(commandDbContext)
                => commandDbContext = _commandDbContext;
    }
}
