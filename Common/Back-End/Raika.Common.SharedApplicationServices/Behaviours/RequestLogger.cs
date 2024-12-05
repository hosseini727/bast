using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Services;
using System.Reflection;
using System.Text;

namespace Raika.Common.SharedApplicationServices.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrentApplicationService _currentApplicationServices;
        public RequestLogger(
            ICurrentUserService currentUserService,
            ILogger<TRequest> logger,
            ICurrentApplicationService currentApplicationServices)
        {
            _currentUserService = currentUserService;
            _logger = logger;
            _currentApplicationServices = currentApplicationServices;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Type requestType = request!.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(requestType.GetProperties());
            StringBuilder requestProperties = new();
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null)!;
                requestProperties.Append($"Property Name: {prop.Name} : Property Value: {propValue}, ");
            }
            _logger.LogInformation("Handling {@ApplicationName} Request: {@Request} - Properties:  {@Properties}",
                    _currentApplicationServices.ApplicationName,
                    typeof(TRequest).Name,
                    requestProperties.ToString());
            return Task.CompletedTask;
        }
    }
}
