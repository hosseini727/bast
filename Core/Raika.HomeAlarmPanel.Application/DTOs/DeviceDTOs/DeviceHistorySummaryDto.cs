using Raika.HomeAlarmPanel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.DTOs.DeviceDTOs
{
    public class DeviceHistorySummaryDto
    {
        public string IMEICode { get; set; }
        public string TechniciansName { get; set; }
        public Guid TechniciansId { get; set; }

  
    }
}
