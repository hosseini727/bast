using Raika.Common.SharedKernel;

namespace Raika.Common.SharedApplicationServices.Services
{
    public interface IJwtTokenService
    {
        Task<JWTTokenBase> CreateAccessTokenAsync(Guid userId);
        Task<JWTTokenBase> CreateRefreshTokenAsync(string expiredAccessToken);
        Task<bool> IsAccessTokenValidAsync(string accessToken);
        Task<bool> IsRefreshTokenValidAsync(Guid userId, string refreshToken);
        Task InvalidateAllActiveTokensAsync(Guid userId);
    }
}
