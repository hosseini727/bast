using Hangfire.Dashboard;

namespace Raika.HomeAlarmPanel.ApiBase.Services
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("HangfireDashboardAdministrator"))
            {
                return true;
            }
            return false;
        }
    }
}
