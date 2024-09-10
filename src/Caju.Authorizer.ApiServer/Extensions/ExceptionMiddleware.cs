using Microsoft.AspNetCore.Diagnostics;

namespace Caju.Authorizer.ApiServer.Extensions
{
    public static class ExceptionMiddleware
    {
        public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
        {
            app.UseExceptionHandler("/error");

            app.Map("/error", (HttpContext context) =>
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (ex is null)
                {
                    return Results.Problem("An unexpected error occurred.");
                }

                return Results.Ok(new { Code = "07" });
            });

            return app;
        }
    }
}
