using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Device;


namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.CreateDevice
{
    public class AddDeviceCommandHandler
      : CommandQueryHandlerBase<AddDeviceCommand, AddDeviceCommandResponse>
       , IRequestHandler<AddDeviceCommand, AddDeviceCommandResponse>
    {
        private readonly IDeviceCommandRepository _deviceCommandRepository;
        private readonly IDeviceQueryRepository _deviceQueryRepository;

        public AddDeviceCommandHandler(
            ILogger<AddDeviceCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
            IDeviceCommandRepository deviceCommandRepository,
            IDeviceQueryRepository deviceQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceCommandRepository = deviceCommandRepository;
            _deviceQueryRepository = deviceQueryRepository;
        }

        public async Task<AddDeviceCommandResponse> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
        {
            AddDeviceCommandResponse response = new();
            try
            {            
                CreateDeviceParameterObject parameterObject = new()
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
                    TypeList = request.TypeList,
                    StoreId = request.StoreId,
                    LastOperation = request.LastOperation,
                    LastOperationDate = request.LastOperationDate,
                    GuarantyStatus = request.GuarantyStatus,
                    BlockStatus = request.BlockStatus,
                };

                var newApplicationRole = Device.Create(parameterObject);
                await _deviceCommandRepository.AddAsync(newApplicationRole);
                await _deviceCommandRepository.SaveChangeAsync();

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
