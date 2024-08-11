using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Credo.API.Helpers;

public class EnumSchema : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            //schema.Type = "string";
            schema.Enum = context.Type.GetEnumValues()
                .Cast<object>()
                .Select(value => new OpenApiString(value.ToString()))
                .Cast<IOpenApiAny>()
                .ToList();
        }
    }
}