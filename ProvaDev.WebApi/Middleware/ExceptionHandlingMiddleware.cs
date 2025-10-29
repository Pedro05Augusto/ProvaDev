using ProvaDev.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProvaDev.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Recurso não encontrado");
        }
        catch (DomainValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (FluentValidation.ValidationException ex)
        {
            await HandleFluentValidationExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string title)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = title,
            message = ex.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, DomainValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new
        {
            error = "Validação falhou",
            errors = ex.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task HandleFluentValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new
        {
            error = "Validação falhou",
            errors = ex.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
