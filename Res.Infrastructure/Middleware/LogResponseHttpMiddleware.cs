using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Res.Infrastructure.Middleware;

public class LogResponseHttpMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ILogger<LogResponseHttpMiddleware> _logger;

    public LogResponseHttpMiddleware(RequestDelegate request, ILogger<LogResponseHttpMiddleware> logger)
    {
        this._request = request;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var ms = new MemoryStream())
        {
            var body = context.Response.Body;
            context.Response.Body = ms;

            await _request(context);

            ms.Seek(0, SeekOrigin.Begin);

            var response = new StreamReader(ms).ReadToEnd();
            ms.Seek(0, SeekOrigin.Begin);

            await ms.CopyToAsync(body);
            context.Response.Body = body;

            _logger.LogInformation(response);
        }
    }
}

public static class LogResponseHttpMiddlewareExtension
{
    public static IApplicationBuilder UseLogResponseHttp(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogResponseHttpMiddleware>();
    }
}