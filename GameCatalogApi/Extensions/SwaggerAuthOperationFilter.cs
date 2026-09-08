using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GameCatalogApi.Extensions;

/// <summary>
/// Adds the Bearer security requirement to every Swagger operation
/// that has an [Authorize] attribute, so Swagger UI attaches the token.
/// </summary>
public class SwaggerAuthOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if controller or action has [Authorize]
        var hasAuthorize =
            context.MethodInfo.DeclaringType!
                .GetCustomAttributes<AuthorizeAttribute>(inherit: true).Any() ||
            context.MethodInfo
                .GetCustomAttributes<AuthorizeAttribute>(inherit: true).Any();

        if (!hasAuthorize)
            return;

        // Apply Bearer security requirement at the operation level with hostDocument passed so reference resolves
        var schemeRef = new OpenApiSecuritySchemeReference("Bearer", context.Document);

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                { schemeRef, new List<string>() }
            }
        };
    }
}
