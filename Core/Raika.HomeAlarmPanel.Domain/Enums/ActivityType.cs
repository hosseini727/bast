using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Domain.Enums
{
    public enum ActivityType
    {
        [Display(Name = "ActiveDevice")]
        ActiveDevice = 1,
        [Display(Name = "AddNewDeviceInPannel")]
        AddNewDeviceInPannel = 2,
        [Display(Name = "UpdateDeviceInPannel")]
        UpdateDeviceInPannel = 3,
        [Display(Name = "DeleteDeviceInPannel")]
        DeleteDeviceInPannel = 4,
        [Display(Name = "RequestingTechnicianNumberFromConsumer")]
        RequestingTechnicianNumberFromConsumer = 5,
    }
}
