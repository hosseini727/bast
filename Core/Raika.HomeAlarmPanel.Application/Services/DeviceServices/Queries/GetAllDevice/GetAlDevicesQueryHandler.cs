using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;
using Raika.HomeAlarmPanel.Application.Mappers;
using Raika.HomeAlarmPanel.Domain.Repositories.QueryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Application.Services.DeviceServices.Queries.GetAllDevice
{
    public class GetAlDevicesQueryHandler
      : CommandQueryHandlerBase<GetAllDeviceQuery, GetAllDevicesQueryResponse>
      , IRequestHandler<GetAllDeviceQuery, GetAllDevicesQueryResponse>
    {
        private readonly IDeviceQueryRepository _deviceQueryRepository;

        #region Constructor
        public GetAlDevicesQueryHandler(
            ILogger<GetAllDeviceQuery> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService,
             IDeviceQueryRepository deviceQueryRepository) : base(logger, currentApplicationService, currentUserService)
        {
            _deviceQueryRepository = deviceQueryRepository;
        }
        #endregion

        public async Task<GetAllDevicesQueryResponse> Handle(GetAllDeviceQuery request, CancellationToken cancellationToken)
        {
            GetAllDevicesQueryResponse response = new();
            try
            {
                var devices = await _deviceQueryRepository.GetAllAsync();
                response.Success = true;
                response.Devices = devices.ConvertToDeviceSummaryDtos();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                await LogApplicationError(request, ex);
                return response;
            }
        }
    }

}
