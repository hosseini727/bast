using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedKernel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Raika.HomeAlarmPanel.ApiBase.Services
{
    public class TokenService : IJwtTokenService
    {
        private readonly IMediator _mediator;
        //private readonly IUserTokenQueryRepository _userTokenQueryRepository;
        //private readonly IUserTokenCommandRepository _userTokenCommandRepository;
        private readonly IConfiguration _configuration;
        private readonly ICurrentApplicationService _currentApplicationService;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IMediator mediator, /*IUserTokenQueryRepository userTokenQueryRepository*/
            IConfiguration configuration,
            ICurrentApplicationService currentApplicationService, ILogger<TokenService> logger
            /*IUserTokenCommandRepository userTokenCommandRepository*/)
        {
            _mediator = mediator;
            //_userTokenQueryRepository = userTokenQueryRepository;
            _configuration = configuration;
            _currentApplicationService = currentApplicationService;
            _logger = logger;
            //_userTokenCommandRepository = userTokenCommandRepository;
        }
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
        /*
        public async Task<JWTTokenBase> CreateAccessTokenAsync(List<Claim> claims, string browserName, string deviceType, string userIp)
        {
            var userIdString = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            try
            {
                var accessTokenExpiresAt = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:AccessTokenExpirationMinutes"]));
                var refreshTokenExpiresAt = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:RefreshTokenExpirationMinutes"]));
                var userId = Guid.Parse(userIdString);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: accessTokenExpiresAt,
                signingCredentials: creds);
                var tokenHandler = new JwtSecurityTokenHandler();

                var accessToken = new JWTTokenBase { AccessToken = tokenHandler.WriteToken(token), UserId = userId };

                //
                // Create refresh token
                //
                var refreshToken = GenerateRefreshToken();
                CreateTokenCommand createTokenCommand = new()
                {
                    UserId = userId,
                    AccessToken = accessToken.AccessToken,
                    AccessTokenExpiresAt = accessTokenExpiresAt,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiresAt = refreshTokenExpiresAt,
                    BrowserName = browserName,
                    DeviceType = deviceType,
                    UserIp = userIp,
                    IsActive = true
                };
                CreateTokenCommandResponse createTokenResponse = await _mediator.Send(createTokenCommand);
                if (!createTokenResponse.Success)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                return new JWTTokenBase
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    UserId = userId,
                    RefreshToken = refreshToken,
                    SuccessFullyCreated = true,
                    TokenId = createTokenResponse.TokenId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("{@ApplicationName} Error in Request: CreateAccessToken UserId: {@UserId}, UserIP: {@UserIP}, Exception: {@Exception}",
                                    _currentApplicationService.ApplicationName, userIdString, userIp, ex.Message);
                return new JWTTokenBase() { SuccessFullyCreated = false };
            }
        }       
        public async Task<JWTTokenBase> CreateRefreshTokenAsync(string expiredAccessToken, string browserName, string deviceType, string userIp)
        {
            try
            {
                var claims = GetPrincipalFromExpiredToken(expiredAccessToken);
                if (claims == null)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                var userId = Guid.Parse(claims.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
                var expiredToken = await _userTokenQueryRepository
                    .FindByConditionAsync(x => x.UserId == userId && x.IsActive == true && x.AccessToken == expiredAccessToken);
                if (expiredToken == null)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                var expiredTokenId = expiredToken.First().UserTokenId;

                //
                // Getting if expired or invalid token
                //            
                if (expiredToken == null)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                if (expiredToken.First().BrowserName != browserName)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                if (expiredToken.First().DeviceType != deviceType)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                if (expiredToken.First().UserIp != userIp)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                //
                // Generate new access token
                //
                var accessTokenExpiresAt = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:AccessTokenExpirationMinutes"]));
                var refreshTokenExpiresAt = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:RefreshTokenExpirationMinutes"]));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        claims: claims.Claims,
                        notBefore: DateTime.Now,
                        expires: accessTokenExpiresAt,
                        signingCredentials: creds);
                var tokenHandler = new JwtSecurityTokenHandler();
                var accessToken = new JWTTokenBase { AccessToken = tokenHandler.WriteToken(token), UserId = expiredToken.First().UserId };

                //
                // Create refresh token
                //
                var refreshToken = GenerateRefreshToken();
                CreateTokenCommand createTokenCommand = new()
                {
                    UserId = expiredToken.First().UserId,
                    AccessToken = accessToken.AccessToken,
                    AccessTokenExpiresAt = accessTokenExpiresAt,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiresAt = refreshTokenExpiresAt,
                    BrowserName = browserName,
                    DeviceType = deviceType,
                    UserIp = userIp,
                };
                CreateTokenCommandResponse createTokenResponse = await _mediator.Send(createTokenCommand);
                if (!createTokenResponse.Success)
                    return new JWTTokenBase() { SuccessFullyCreated = false };
                return new JWTTokenBase
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    UserId = expiredToken.First().UserId,
                    RefreshToken = refreshToken,
                    SuccessFullyCreated = true,
                    TokenId = createTokenResponse.TokenId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("{@ApplicationName} Error in Request: CreateRefreshTokenAsync Token: {@Token}, UserIP: {@UserIP}, Exception: {@Exception}",
                                     _currentApplicationService.ApplicationName, expiredAccessToken, userIp, ex.Message);
                return new JWTTokenBase() { SuccessFullyCreated = false };
            }

        }
        public async Task<bool> DeactivateAllUserTokensAsync(Guid userId)
        {
            var result = await _userTokenCommandRepository.DeactivateAllUserActiveTokensAsync(userId);
            return true;
        }
        public async Task<bool> IsValidAccessTokenAsync(Guid tokenId)
        {
            UserToken token = await _userTokenQueryRepository.FindByKeyAsync(tokenId);
            return token?.AccessTokenExpiresAt >= DateTime.Now;
        }
        */
        //
        // Private methods
        //
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var encryptionkey = Encoding.UTF8.GetBytes(_configuration["JWT:EnKey"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateIssuer = false, // on production make it true
                ValidateAudience = false, // on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                //throw new SecurityTokenException("Invalid token");
                return null;
            }
            return principal;
        }
    }
}
