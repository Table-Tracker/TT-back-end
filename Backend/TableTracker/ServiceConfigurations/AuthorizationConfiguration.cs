using System;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using TableTracker.Infrastructure;
using TableTracker.Infrastructure.Identity;
using TableTracker.JwtFeatures;

namespace TableTracker.ServiceConfigurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<JwtHandler>();

            services.AddIdentity<TableTrackerIdentityUser, TableTrackerIdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<IdentityTableDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<TableTrackerIdentityUser, TableTrackerIdentityRole, IdentityTableDbContext, Guid>>()
                .AddRoleStore<RoleStore<TableTrackerIdentityRole, IdentityTableDbContext, Guid>>();

            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddAuthorization(opt =>
            {
                opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}
