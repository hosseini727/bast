using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Exceptions;

namespace Raika.HomeAlarmPanel.ApiBase.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ProblemDetails details = new();
            try
            {
                await next(context);
            }
            catch (ApplicationServiceExeptionBase ex)
            {
                details.Status = (int)context.Response.StatusCode;
                details.Type = "Application service exception";
                details.Detail = ex.Message;
            }
            catch (FluentValidation.ValidationException ex)
            {
                details.Status = 500;
                await context.Response.WriteAsJsonAsync(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                details.Status = 500;
                await context.Response.WriteAsJsonAsync(ex.Message);
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
