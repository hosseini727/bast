using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Raika.HomeAlarmPanel.ApiBase.Headers
{
    public class LocalizationHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "culture",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Default = new OpenApiString("fa")
                },
                Description = "culture",
                Required = true
            });
        }
    }
}
