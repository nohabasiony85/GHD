using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.App.Exceptions;
using ProductManagement.App.Models;

namespace ProductManagement.API.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        var code = exception switch
        {
            ValidationException or BadHttpRequestException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            NotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        return (code, JsonConvert.SerializeObject(new ApiResponse(exception.Message)));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            logger.LogInformation($"Request {context.Request.Method}: {context.Request.Path}");

            await next(context);
        }
        catch (Exception exception)
        {
            // Log the error
            logger.LogError(exception, exception.Message);
            
            var exceptionDetails = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
            };

            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    
    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors has occurred",
                validationException.Errors),
            NotFoundException notFoundException => new ExceptionDetails(
                StatusCodes.Status404NotFound,
                "NotFound",
                "Not Found error",
                notFoundException.Message,
                null),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error has occurred",
                null)
        };
    }
}