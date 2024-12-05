using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedKernel;

namespace Raika.HomeAlarmPanel.ApiBase.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public Task<JWTTokenBase> CreateAccessTokenAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<JWTTokenBase> CreateRefreshTokenAsync(string expiredAccessToken)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateAllActiveTokensAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAccessTokenValidAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRefreshTokenValidAsync(Guid userId, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
