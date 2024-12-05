using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Services;
using System.Text.Json;

namespace Raika.Common.SharedApplicationServices.Common
{
    public class CommandQueryHandlerBase<T, TResponse> where T : IRequest<TResponse>
    {
        private readonly ILogger<T> _logger;
        private readonly ICurrentApplicationService _currentApplicationService;
        private readonly ICurrentUserService _currentUserService;

        public CommandQueryHandlerBase(
            ILogger<T> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentApplicationService = currentApplicationService;
            _currentUserService = currentUserService;
        }
        protected async Task LogApplicationError(T request, Exception ex)
        {
            _logger.LogError($"Error in {typeof(T).Name}, UserId : {_currentUserService.UserId}, ApplicationName: {_currentApplicationService.ApplicationName}, " +
                $"ApplicationId: {_currentApplicationService.ApplicationId}, Request: {JsonSerializer.Serialize(request)}, " +
                $"Exception Message: {ex.Message}");
            await Task.CompletedTask;
        }
        protected async Task LogData(string data) 
        {
            _logger.LogInformation(data);
            await Task.CompletedTask;
        }
    }
}
