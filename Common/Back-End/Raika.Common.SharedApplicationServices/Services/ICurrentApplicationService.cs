namespace Raika.Common.SharedApplicationServices.Services
{
    public interface ICurrentApplicationService
    {
        Guid ApplicationId { get; }
        string ApplicationName { get; }
        string DefaultCompanyAvatar { get; }
        string DefaultUserAvatar { get; }
    }
}
