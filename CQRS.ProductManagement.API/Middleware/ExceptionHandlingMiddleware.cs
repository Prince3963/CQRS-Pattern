using CQRS.ProductManagement.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CQRS.ProductManagement.API.Middleware
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException exception)
            {
                await WriteValidationProblemAsync(context, exception);
            }
            catch (NotFoundException exception)
            {
                await WriteProblemAsync(context, StatusCodes.Status404NotFound, exception.Message);
            }
            catch (Exception)
            {
                await WriteProblemAsync(
                    context,
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private static Task WriteValidationProblemAsync(HttpContext context, ValidationException exception)
        {
            var errors = exception.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "Validation failed.",
                Status = StatusCodes.Status400BadRequest,
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsJsonAsync(problemDetails);
        }

        private static Task WriteProblemAsync(HttpContext context, int statusCode, string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ReasonPhrases.GetReasonPhrase(statusCode),
                Status = statusCode,
                Detail = detail,
            };

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
