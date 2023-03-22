using Microsoft.Extensions.DependencyInjection;

using TableTracker.Application.MapperProfiles;

namespace TableTracker.ServiceConfigurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(FranchiseProfile).Assembly);
        }
    }
}
