using Microsoft.AspNetCore.Http;
using Raika.Common.SharedApplicationServices.Services;

namespace Raika.HomeAlarmPanel.ApiBase.Services
{
    public class CurrentApplicationServices : ICurrentApplicationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentApplicationServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //var httpContext = httpContextAccessor?.HttpContext;

            //if(httpContext != null) 
            //{
            //    ApplicationName = httpContext?.Items["Appname"]?.ToString() ?? "";// "RaikaFlowFront";
            //    DefaultCompanyAvatar = "";
            //    DefaultUserAvatar = "";
            //    ApplicationId = string.IsNullOrEmpty(httpContext?.Items?["Appid"]?.ToString()) ? Guid.Parse(httpContext?.Items?["Appid"]?.ToString()!) : Guid.Empty;
            //}
            //Guid.Parse("69941524-eb25-4d88-929a-c0e8041c581e");
        }
        public string ApplicationName
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null)
                    return "RaikaFlowFront";
                else if (_httpContextAccessor.HttpContext?.Items["Appname"] is string appName)
                    return appName;
                return "RaikaFlowFront";
            }
        }
        public string DefaultCompanyAvatar { get; } = string.Empty;
        public string DefaultUserAvatar { get; } = string.Empty;
        public Guid ApplicationId
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null)
                    return Guid.Empty;
                else if (_httpContextAccessor.HttpContext?.Items["Appid"] is Guid appId)
                    return appId;
                return Guid.Empty;
            }
        }
    }
}
