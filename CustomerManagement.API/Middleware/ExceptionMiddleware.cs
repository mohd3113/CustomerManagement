using CustomerManagement.Application.Exceptions;
using System.Net;

namespace CustomerManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorMessage = exception.Message;
            var errorType = "Error";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorType = "Not Found";
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorType = "Unauthorized";
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorType = "Validation Error";
                    errorMessage = string.Join(",", validationException.Errors);
                    break;
                default:
                    _logger.LogError(exception, exception.Message);
                    break;

            }

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(new { error = errorType, message = errorMessage });

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);

        }
    }
}