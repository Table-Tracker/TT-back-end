using Microsoft.AspNetCore.Builder;

namespace TableTracker.ServiceConfigurations
{
    public static class CorsConfiguration
    {
        public static IApplicationBuilder UseCrossOriginResourceSharing(this IApplicationBuilder app)
        {
            return app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
        }
    }
}
