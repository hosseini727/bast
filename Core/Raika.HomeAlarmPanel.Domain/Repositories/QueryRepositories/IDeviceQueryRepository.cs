using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories
{
    public interface IDeviceQueryRepository : IQueryGenericRepository<Device, Guid>
    {

    }
}
