using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Raika.HomeAlarmPanel.ApiBase.Middlewares
{
    public class JwtValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtValidatorMiddleware> _logger;

        public JwtValidatorMiddleware(
            RequestDelegate next,
            ILogger<JwtValidatorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                await ValidateTokenAsync(context, serviceProvider);
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid JWT token.");
                return;
            }
            await _next(context);
        }

        private async Task ValidateTokenAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var appId = context.Request.Headers["AppId"].FirstOrDefault() ?? "69941524-eb25-4d88-929a-c0e8041c581e";

            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    //var repository = scope.ServiceProvider.GetRequiredService<IApplicationQueryRepository>();
                    //var application = await repository.FindByKeyAsync(Guid.Parse(appId), "Id");
                    var key = Encoding.UTF8.GetBytes("");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    if (!tokenHandler.CanReadToken(token))
                        throw new SecurityTokenException("Malformed token.");

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "",//application.JwtTokenIssuer,
                        ValidAudience = "",//application.JwtTokenAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                    }, out SecurityToken validatedToken);
                }
                catch (SecurityTokenExpiredException)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("{\"message\":\"Token has expired.\"}");
                }
                catch (SecurityTokenException ex)
                {
                    _logger.LogError(ex, "JWT validation failed.");
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"Unauthorized: Invalid JWT token.\"}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during JWT validation.");
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"Internal Server Error.\"}");
                }
            }
        }
    }
}
