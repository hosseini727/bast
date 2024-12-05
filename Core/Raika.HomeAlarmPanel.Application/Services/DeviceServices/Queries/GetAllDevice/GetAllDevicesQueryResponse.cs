using Raika.Common.SharedApplicationServices.Common;
using Raika.HomeAlarmPanel.Application.DTOs.DeviceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Queries.GetAllDevice
{
    public class GetAllDevicesQueryResponse : QueryResponseBase
    {
        public List<DeviceSummaryDto> Devices { get; set; }

    }
}
