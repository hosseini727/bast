using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Infrastructure.Repositories.CommandRepositories
{
    public class DeviceHistoryInvoiceCommandRepository : CommandRepositoryBase<DeviceHistory>, IDeviceHistoryCommandRepository
    {
        private readonly CommandDbContext _commandDbContext;
        public DeviceHistoryInvoiceCommandRepository(CommandDbContext commandDbContext) : base(commandDbContext)
                => commandDbContext = _commandDbContext;
    }
}
