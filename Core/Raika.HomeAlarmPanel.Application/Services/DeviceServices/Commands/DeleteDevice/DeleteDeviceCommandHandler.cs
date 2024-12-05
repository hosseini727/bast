using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.ParameterObjects;
using Raika.HomeAlarmPanel.Domain.Repositories.CommandRepositories;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;


namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.DeleteDevice
{   
    public class DeleteDeviceCommandHandler:
        CommandQueryHandlerBase<DeleteDeviceCommand, DeleteDeviceCommandResponse>
       ,IRequestHandler<DeleteDeviceCommand, DeleteDeviceCommandResponse>
    {
        private readonly IDeviceCommandRepository _deviceCommandRepository;
        private readonly IDeviceQueryRepository _deviceQueryRepository;

        #region Constructor
        public DeleteDeviceCommandHandler(
            ILogger<DeleteDeviceCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
            IDeviceCommandRepository deviceCommandRepository,
            IDeviceQueryRepository deviceQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceCommandRepository = deviceCommandRepository;
            _deviceQueryRepository = deviceQueryRepository;
        }
        #endregion

        public async Task<DeleteDeviceCommandResponse> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            DeleteDeviceCommandResponse response = new();
            try
            {
                var existingDevice = await _deviceQueryRepository.FindByKeyAsync(request.DeviceId, cancellationToken);

               // existingDevice.Delete();

                await _deviceCommandRepository.SoftDeleteAsync(existingDevice);
                await _deviceCommandRepository.SaveChangeAsync(cancellationToken);
                response.Success = true;
            }
            catch (Exception ex)
            {
                await LogApplicationError(request, ex);
            }
            return response;
        }
    }
}
