using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace HR.LeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            ErrorDetails errorDetails = new()
            {
                Message = "An unexpected error occurred.",
                Type = "Internal Failure"
            };

            switch (ex)
            {
                case BadRequestException badRequestException:

                    errorDetails.Message = ex.Message;

                    if (badRequestException.ValidationErrors is not null && badRequestException.ValidationErrors.Any())
                    {
                        foreach (var validationError in badRequestException.ValidationErrors)
                        {
                            errorDetails.Details.Add(validationError);
                        }
                    }
                    errorDetails.Type = "Bad Request";
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case NotFoundException notFoundException:
                    errorDetails.Message = notFoundException.Message;
                    errorDetails.Type = "Not Found";
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case BadTransactionEcxeption badTransactionEcxeption:
                    errorDetails.Message = ex.Message;
                    errorDetails.Type = "Bad Transaction";
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

                default:
                    break;
            }
            var response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }
    }
}