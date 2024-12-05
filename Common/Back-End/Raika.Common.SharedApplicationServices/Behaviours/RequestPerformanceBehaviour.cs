using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raika.Common.SharedApplicationServices.Services;
using Serilog.Context;
using System.Diagnostics;
using System.Text.Json;

namespace Raika.Common.SharedApplicationServices.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrentApplicationService _currentApplicationServices;

        public RequestPerformanceBehaviour(
            ICurrentUserService currentUserService,
            ILogger<TRequest> logger,
            ICurrentApplicationService currentApplicationServices)
        {
            _timer = new Stopwatch();
            _currentUserService = currentUserService;
            _logger = logger;
            _currentApplicationServices = currentApplicationServices;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 300)
            {
                var name = typeof(TRequest).Name;
                LogContext.PushProperty("Username", _currentUserService.Username);
                LogContext.PushProperty("UserIp", _currentUserService.UserIp);
                _logger.LogWarning($"Long Running Request in : {_currentApplicationServices.ApplicationName}, " +
                    $"Time: {_timer.ElapsedMilliseconds} milliseconds, " +
                    $"Request name: {name}, " +                    
                    $"Request data: {JsonSerializer.Serialize(request, new JsonSerializerOptions() { })}, " +
                    $"User Id: {_currentUserService.UserId}, " +
                    $"User name: {_currentUserService.Username}");
            }
            return response;
        }
    }
}
