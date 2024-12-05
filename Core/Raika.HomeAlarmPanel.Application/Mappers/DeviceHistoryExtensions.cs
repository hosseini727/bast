using Raika.HomeAlarmPanel.Application.DTOs.DeviceDTOs;
using Raika.HomeAlarmPanel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Mappers
{
   
    public static class DeviceHistoryExtensions
    {
        public static DeviceHistorySummaryDto ConvertToDeviceSummaryDto(this DeviceHistory deviceHistory)
        {
            return new DeviceHistorySummaryDto()
            {
               IMEICode = deviceHistory.IMEICode,
               TechniciansId = deviceHistory.TechniciansId,
               TechniciansName= deviceHistory.TechniciansName,
            };
        }


        public static List<DeviceHistorySummaryDto> ConvertToDeviceSummaryDtos(this IEnumerable<DeviceHistory> deviceHistories)
        {
            var result = new List<DeviceHistorySummaryDto>();
            foreach (var deviceHistory in deviceHistories)
                result.Add(deviceHistory.ConvertToDeviceSummaryDto());
            return result;
        }
    }

}
