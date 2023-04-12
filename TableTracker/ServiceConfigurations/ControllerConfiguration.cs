using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace TableTracker.ServiceConfigurations
{
    public static class ControllerConfiguration
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            return services;
        }
    }
}
