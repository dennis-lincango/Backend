using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Extensions
{
    /// <summary>
    /// Provides extension methods for handling exceptions in the application.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configures the exception handler for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="logger">The logger for logging exceptions.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync($"{context.Response.StatusCode} - Internal Server Error");
                    }
                });
            });
        }
    }
}