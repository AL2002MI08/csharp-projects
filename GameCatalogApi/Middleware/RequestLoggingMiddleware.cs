namespace GameCatalogApi.Middleware;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"→ {context.Request.Method} {context.Request.Path}");
        await _next(context);  // pass to next middleware
        Console.WriteLine($"← {context.Response.StatusCode}");
    }
}
