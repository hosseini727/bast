using FluentValidation;
using Raika.HomeAlarmPanel.Application.Services.DeviceHistoryServices.AddDeviceHistoryServices;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.CreateDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.InvoiceServices.AddInvo
{
    public class AddDeviceHistoryCommandValidator : AbstractValidator<AddDeviceHistoryCommand>
    {
        public AddDeviceHistoryCommandValidator()
        {
                
        }
    }
}
