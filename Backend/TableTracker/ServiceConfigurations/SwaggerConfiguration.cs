using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TableTracker.ServiceConfigurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TableTracker", Version = "v1" });
                c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "{token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Token",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
