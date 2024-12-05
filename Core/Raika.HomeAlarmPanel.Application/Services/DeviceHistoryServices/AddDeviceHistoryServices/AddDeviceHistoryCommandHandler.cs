using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Application.Services.DeviceHistoryServices.AddDeviceHistoryServices;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.CreateDevice;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.Enums;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Device;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.DeviceHistory;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.InvoiceServices.AddInvoice
{
    internal class AddDeviceHistoryCommandHandler
      : CommandQueryHandlerBase<AddDeviceHistoryCommand, AddDeviceHistoryCommandResponse>
       , IRequestHandler<AddDeviceHistoryCommand, AddDeviceHistoryCommandResponse>
    {
        private readonly IDeviceHistoryCommandRepository _deviceHistoryCommandRepository;
        private readonly IDeviceHistoryQueryRepository _deviceHistoryQueryRepository;

        public AddDeviceHistoryCommandHandler(
            ILogger<AddDeviceHistoryCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
            IDeviceHistoryCommandRepository deviceHistoryCommandRepository,
            IDeviceHistoryQueryRepository deviceHistoryQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceHistoryCommandRepository = deviceHistoryCommandRepository;
            _deviceHistoryQueryRepository = deviceHistoryQueryRepository;
        }
        public async Task<AddDeviceHistoryCommandResponse> Handle(AddDeviceHistoryCommand request, CancellationToken cancellationToken)
        {
            AddDeviceHistoryCommandResponse response = new();
            try
            {
                CreateDeviceHistoryParameterObject parameterObject = new()
                {
                    IMEICode = request.IMEICode,
                    DeviceType = request.DeviceType,
                    Brand = request.Brand,
                    Model = request.Model,
                    Version = request.Version,
                    FirstProductionSeries = request.FirstProductionSeries,
                    ProductionSeries = request.ProductionSeries,
                    SimCardNumber = request.SimCardNumber,
                    UsersCellPhoneNumber = request.UsersCellPhoneNumber,
                    ActivationCode = request.ActivationCode,
                    CustomersId = request.CustomersId,
                    CustomersName = request.CustomersName,
                    TechniciansId = request.TechniciansId,
                    TechniciansName = request.TechniciansName,
                    TechnicianCellPhoneNumber = request.TechnicianCellPhoneNumber,
                    TechnicianVertificationStatus = request.TechnicianVertificationStatus,
                    RetailersCity = request.RetailersCity,
                    StoreCode = request.StoreCode,
                    InvoiceId = request.InvoiceId,
                    InvoiceNumber = request.InvoiceNumber,
                    RetailersId = request.RetailersId,
                    RetailersName = request.RetailersName,
                    TypeList = request.TypeList ?? new List<TypeList>(), 
                    StoreId = request.StoreId,
                    LastOperation = request.LastOperation,
                    LastOperationDate = request.LastOperationDate,
                    GuarantyStatus = request.GuarantyStatus,
                    BlockStatus = request.BlockStatus,
                    HistoryId = request.HistoryId,
                    ActivityTypes = request.ActivityTypes,
                    OriginCausingActivity = request.OriginCausingActivity,
                    ListLogsOrgin = request.ListLogsOrgin,
                    RefrenceId = request.RefrenceId,
                    ChangeDateTime = request.ChangeDateTime
                };
                var newApplicationRole = DeviceHistory.Create(parameterObject);
                await _deviceHistoryCommandRepository.AddAsync(newApplicationRole);
                await _deviceHistoryCommandRepository.SaveChangeAsync();

                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                await LogApplicationError(request, ex);
            }
            response.Success = false;
            return response;
        }

    }    
}
