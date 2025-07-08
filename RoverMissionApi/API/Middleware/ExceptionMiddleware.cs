using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    // Constructor que recibe el siguiente middleware y el logger
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Método InvokeAsync correcto
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = ex switch
            {
                TaskConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
    }
}