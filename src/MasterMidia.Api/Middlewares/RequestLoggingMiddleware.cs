using System;
using System.Diagnostics;
using System.Text;

namespace MasterMidia.Api.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();
        
        var stopwatch = Stopwatch.StartNew();

        var requestLog = await FormatRequest(context.Request);
        _logger.LogInformation(">>> REQ INICIAL: {Method} {Path} -> {Body}", 
            context.Request.Method, context.Request.Path, requestLog);

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation("<<< REQ FINALIZADA: {Method} {Path} | Status: {StatusCode} | Tempo: {ElapsedMs}ms",
            context.Request.Method, context.Request.Path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }

    private static async Task<string> FormatRequest(HttpRequest request)
    {
        // Se não for POST ou PUT, geralmente não há corpo relevante
        if (request.Method != "POST" && request.Method != "PUT")
        {
            return string.Empty;
        }

        // Lemos o corpo da requisição
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadExactlyAsync(buffer.AsMemory(0, buffer.Length));
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        
        // Volta o Stream para o início para que o Controller possa lê-lo
        request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText.Length > 256 ? bodyAsText.Substring(0, 256) + "..." : bodyAsText;
    }
}