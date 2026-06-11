using ProductManagementSystem.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductManagementSystem.API.Middleware
{
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
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new
                    {
                        Message = ex.Message
                    }));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new
                    {
                        Message = ex.Message
                    }));
            }
        }
    }
}
