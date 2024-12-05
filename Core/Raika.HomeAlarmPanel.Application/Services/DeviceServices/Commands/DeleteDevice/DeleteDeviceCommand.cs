using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.DeleteDevice
{
    public class DeleteDeviceCommand : IRequest<DeleteDeviceCommandResponse>
    {
        public Guid DeviceId { get; set; }
    }

}
