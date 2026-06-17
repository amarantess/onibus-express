using System.Net;
using System.Text.Json;

using FluentValidation;

using OnibusExpress.Api.Responses;
using OnibusExpress.Infrastructure.Exceptions.ExceptionsBase;

namespace OnibusExpress.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private const string UnexpectedErrorMessage = "An unexpected error occurred.";

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
        catch (OnibusExpressException exception)
        {
            await WriteResponseAsync(
                context,
                (int)exception.GetStatusCode(),
                new ResponseError(exception.GetErrorMessages()));
        }
        catch (ValidationException exception)
        {
            var errors = exception.Errors
                .Select(error => error.ErrorMessage)
                .Distinct()
                .ToList();

            await WriteResponseAsync(
                context,
                (int)HttpStatusCode.BadRequest,
                new ResponseError(errors));
        }
        catch (Exception)
        {
            await WriteResponseAsync(
                context,
                (int)HttpStatusCode.InternalServerError,
                new ResponseError(UnexpectedErrorMessage));
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, int statusCode, ResponseError response)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await JsonSerializer.SerializeAsync(
            context.Response.Body,
            response,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
