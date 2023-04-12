using MediatR;

using Microsoft.Extensions.DependencyInjection;

using TableTracker.Application.CQRS;

namespace TableTracker.ServiceConfigurations
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(CommandResponse<object>).Assembly);
        }
    }
}
