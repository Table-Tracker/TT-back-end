using Microsoft.AspNetCore.Builder;

using TableTracker.Middlewares;

namespace TableTracker.ServiceConfigurations
{
    public static class MiddlewareConfiguration
    {
        public static void UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
