using FluentValidation;
using Raika.HomeAlarmPanel.Application.Localization;
using Raika.HomeAlarmPanel.Application.Services.StoreServices.Commands.AddStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.CreateDevice
{
    public class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
    {
        public AddDeviceCommandValidator()
        {         

            RuleFor(x => x.SimCardNumber)
                .NotEmpty().WithMessage(RaikaHomeAlarmPanelLocalization.SimCardNumberIsRequired)
                .Matches(@"^\d{10}$").WithMessage(RaikaHomeAlarmPanelLocalization.SimCardNumberMust10digitNumber);        
        }
    }
}
