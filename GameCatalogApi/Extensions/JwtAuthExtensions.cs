using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GameCatalogApi.Extensions;

public static class JwtAuthExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var keyString = configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(keyString))
            throw new InvalidOperationException("JWT Secret Key is missing from configuration.");
        var jwtKey = Encoding.UTF8.GetBytes(keyString);
        var signingKey = new SymmetricSecurityKey(jwtKey)
        {
            KeyId = keyString
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = signingKey,
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };
        });

        return services;
    }

    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
