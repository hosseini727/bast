namespace Raika.Common.SharedApplicationServices.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Username { get; }
        string AuthCookieName { get; }
        string AuthCookievalue { get; }
        string UserIp { get; }
    }
}
