using System;
using System.Net;
using System.Text.Json;
using MasterMidia.App.Common.Exceptions;

namespace MasterMidia.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment environment)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IHostEnvironment _environment = environment;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // 1. Logging condicional
        if (_environment.IsDevelopment())
        {
            _logger.LogError(exception, "Exceção tratada na requisição {Path}: {Message}", context.Request.Path, exception.Message);
        }
        else
        {
            _logger.LogWarning("Exceção tratada na requisição {Path}: {Message}", context.Request.Path, exception.Message);
        }

        context.Response.ContentType = "application/json";

        // 2. Mapeamento da Exceção para Status Code e Mensagem
        var (statusCode, message) = exception switch
        {
            UnauthorizedException => (HttpStatusCode.Unauthorized, exception.Message), // 401
            NotFoundException => (HttpStatusCode.NotFound, exception.Message),         // 404
            ConflictException => (HttpStatusCode.Conflict, exception.Message),         // 409
            _ => (HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado no servidor.")
        };

        context.Response.StatusCode = (int)statusCode;

        // 3. Formatação da Resposta JSON
        var problemDetails = new 
        { 
            status = context.Response.StatusCode, 
            title = statusCode.ToString(),
            error = message 
        };

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(json);
    }
}
