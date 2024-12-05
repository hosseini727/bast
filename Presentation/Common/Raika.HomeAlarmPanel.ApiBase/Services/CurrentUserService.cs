using Microsoft.AspNetCore.Http;
using Raika.Common.SharedApplicationServices.Services;
using System.Security.Claims;

namespace Raika.HomeAlarmPanel.ApiBase.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //var httpContext = httpContextAccessor?.HttpContext;
            ////if (httpContext?.User?.Identity?.IsAuthenticated == true)
            ////{
            //if (httpContext != null)
            //{
            //    Username = httpContext.User.FindFirstValue(ClaimTypes.Name) ?? "Anonymous";
            //    UserId = Guid.TryParse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId) ? userId : Guid.Empty;
            //    UserIp = httpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";
            //}
            //}

            //Username = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)!;
            //UserId = Guid.Parse(httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);
            //UserIp = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString()!;
            //UserId = Guid.Parse("474D9334-D158-435D-8B0F-5E35D98B0FDE");
            //AuthCookieName = ".TeamTracker.SharedAuthCookie";
            //AuthCookievalue = httpContextAccessor?.HttpContext?.Request.Cookies[AuthCookieName]?.ToString()!;
            //IsAuthenticated = Username != null;
        }
        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null || _httpContextAccessor.HttpContext?.User is null)
                    return Guid.Empty;
                else
                {
                    var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (Guid.TryParse(userId, out var uid))
                        return uid;
                    return Guid.Empty;
                }

            }
        }
        public string Username
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null || _httpContextAccessor.HttpContext?.User is null)
                    return string.Empty;
                return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? "Anonymous";
            }
        }
        public bool IsAuthenticated { get; } = false;
        public string AuthCookieName { get; } = string.Empty;
        public string AuthCookievalue { get; } = string.Empty;
        public string UserIp
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null)
                    return string.Empty;
                return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";
            }
        }
    }
}
