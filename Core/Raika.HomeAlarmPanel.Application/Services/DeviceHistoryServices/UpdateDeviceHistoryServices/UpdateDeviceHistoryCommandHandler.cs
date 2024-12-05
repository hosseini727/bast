using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Domain.Enums;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.DeviceHistory;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;


namespace Raika.HomeAlarmPanel.Application.Services.DeviceHistoryServices.UpdateDeviceHistoryServices
{
    public class UpdateDeviceHistoryCommandHandler :
        CommandQueryHandlerBase<UpdateDeviceHistoryCommand, UpdateDeviceHistoryCommandResponse>
       , IRequestHandler<UpdateDeviceHistoryCommand, UpdateDeviceHistoryCommandResponse>
    {
        private readonly IDeviceHistoryCommandRepository _deviceHistoryCommandRepository;
        private readonly IDeviceHistoryQueryRepository _deviceHistoryQueryRepository;

        public UpdateDeviceHistoryCommandHandler(
            ILogger<UpdateDeviceHistoryCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
            IDeviceHistoryCommandRepository deviceHistoryCommandRepository,
            IDeviceHistoryQueryRepository deviceHistoryQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceHistoryCommandRepository = deviceHistoryCommandRepository;
            _deviceHistoryQueryRepository = deviceHistoryQueryRepository;
        }


        public async Task<UpdateDeviceHistoryCommandResponse> Handle(UpdateDeviceHistoryCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateDeviceHistoryCommandResponse();
            try
            {
                var existingDeviceHistory = await _deviceHistoryQueryRepository.FindByKeyAsync(request.HistoryId, cancellationToken);
                if (existingDeviceHistory == null)
                {
                    response.Success = false;
                    return response;
                }

                existingDeviceHistory.Update(new UpdateDeviceHistoryParameterObject
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
                    ActivityTypes = request.ActivityTypes,
                    OriginCausingActivity = request.OriginCausingActivity,
                    ListLogsOrgin = request.ListLogsOrgin,
                    RefrenceId = request.RefrenceId,
                    ChangeDateTime = request.ChangeDateTime
                });

                await _deviceHistoryCommandRepository.UpdateAsync(existingDeviceHistory);
                await _deviceHistoryCommandRepository.SaveChangeAsync();
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