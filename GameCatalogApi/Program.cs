using GameCatalogApi.Data;
using GameCatalogApi.Extensions;
using GameCatalogApi.Middleware;
using GameCatalogApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registered as Scoped because AppDbContext is Scoped
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=gamecatalog.db"));

// Extension method for JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter bearer token below."
    });
    // Applies the Bearer lock icon to every [Authorize] endpoint at the operation level
    options.OperationFilter<SwaggerAuthOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthMiddleware();
app.MapControllers();

app.Run();