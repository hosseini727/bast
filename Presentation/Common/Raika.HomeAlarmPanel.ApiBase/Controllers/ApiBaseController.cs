using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedInfrastructure.DateTimeHelper;
using Wangkanai.Detection.Services;

namespace Raika.HomeAlarmPanel.ApiBase.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly IDateTimeHelper _dateTimeHelper;
        protected readonly IDetectionService _detectionService;
        protected readonly ILogger<ApiBaseController> _logger;
        protected readonly ICurrentApplicationService _currentApplicationService;
        protected readonly ICurrentUserService _currentUserService;

        public ApiBaseController(
            IMediator mediator,
            IHttpContextAccessor contextAccessor,
            IDateTimeHelper dateTimeHelper,
            IDetectionService detectionService,
            ILogger<ApiBaseController> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
            _dateTimeHelper = dateTimeHelper;
            _detectionService = detectionService;
            _logger = logger;
            _currentApplicationService = currentApplicationService;
            _currentUserService = currentUserService;
        }

        protected async Task LogApplicationError(string controllerName, Exception ex)
        {
            _logger.LogError($"Error in {controllerName}, UserId : {_currentUserService.UserId}, ApplicationName: {_currentApplicationService.ApplicationName}, " +
                $"ApplicationId: {_currentApplicationService.ApplicationId}, " +
                $"Exception Message: {ex.Message}");
            await Task.CompletedTask;
        }
    }
}
