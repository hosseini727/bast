using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.DeleteDevice;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.ParameterObjects;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Device;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;


namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.UpdateDevice
{    
    public class UpdateDeviceCommandHandler :
        CommandQueryHandlerBase<UpdateDeviceCommand, UpdateDeviceCommandResponse>
       , IRequestHandler<UpdateDeviceCommand, UpdateDeviceCommandResponse>
    {
        private readonly IDeviceCommandRepository _deviceCommandRepository;
        private readonly IDeviceQueryRepository _deviceQueryRepository;

        public UpdateDeviceCommandHandler(
            ILogger<UpdateDeviceCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
            IDeviceCommandRepository deviceCommandRepository,
            IDeviceQueryRepository deviceQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceCommandRepository = deviceCommandRepository;
            _deviceQueryRepository = deviceQueryRepository;
        }


        public async Task<UpdateDeviceCommandResponse> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateDeviceCommandResponse();
            try
            {
                var existingDevice = await _deviceQueryRepository.FindByKeyAsync(request.DeviceId, cancellationToken);
                if (existingDevice == null)
                {
                    response.Success = false;
                    return response;
                }

                existingDevice.Update(new UpdateDeviceParameterObject
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
                });

                await _deviceCommandRepository.UpdateAsync(existingDevice);
                await _deviceCommandRepository.SaveChangeAsync();
                response.Success = true;
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
