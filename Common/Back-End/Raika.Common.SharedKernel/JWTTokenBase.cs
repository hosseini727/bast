namespace Raika.Common.SharedKernel
{
    public class JWTTokenBase
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
