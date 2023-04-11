using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using TableTracker.Application.Services;
using TableTracker.Application.Validators.Restaurants.Commands;

namespace TableTracker.ServiceConfigurations
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddValidaton(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            AssemblyScanner.FindValidatorsInAssemblyContaining<FindRestaurantByIdValidator>()
                .ForEach(result => {
                    services.AddTransient(result.InterfaceType, result.ValidatorType);
                });

            return services;
        }
    }
}
