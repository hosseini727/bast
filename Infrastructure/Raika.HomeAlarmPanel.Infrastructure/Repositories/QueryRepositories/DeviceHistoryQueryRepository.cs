using Dapper;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using Raika.HomeAlarmPanel.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Infrastructure.Repositories.QueryRepositories
{
    public class DeviceHistoryQueryRepository : QueryRepositoryBase<Device, Guid>, IDeviceQueryRepository
    {
        private readonly QueryDbContext _queryDbContext;
        public DeviceHistoryQueryRepository(QueryDbContext queryDbContext) : base(queryDbContext) => _queryDbContext = queryDbContext;
    }
}
