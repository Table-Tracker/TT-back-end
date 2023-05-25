using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Infrastructure;
using TableTracker.Infrastructure.Repositories;

namespace TableTracker.ServiceConfigurations
{
    public static class DataAccessConfiguration
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var _configuration = new ConfigurationBuilder().AddEnvironmentVariables("TT_").Build();


            var ttEnvConnectionString = $"Server={_configuration["MYSQL_SERVERNAME"]}; " +
                $"port={_configuration["MYSQL__PORT"]};" +
                $" Database={_configuration["MYSQL_DATABASE"]};" +
                $"UserId={_configuration["MYSQL_USER"]};" +
                $"Password={_configuration["MYSQL_PASSWORD"]};";

            var ttIdentityEnvConnectionString = $"Server={_configuration["MYSQL_SERVERNAME"]}; " +
                $"port={_configuration["IDENTITY_MYSQL_PORT"]};" +
                $" Database={_configuration["IDENTITY_MYSQL_DATABASE"]};" +
                $"UserId={_configuration["MYSQL_USER"]};" +
                $"Password={_configuration["MYSQL_PASSWORD"]};";

            services.AddDbContext<TableDbContext>(options =>
                options.UseMySql(ttEnvConnectionString, ServerVersion.AutoDetect(ttEnvConnectionString)));

            services.AddDbContext<IdentityTableDbContext>(options =>
                options.UseMySql(ttIdentityEnvConnectionString, ServerVersion.AutoDetect(ttIdentityEnvConnectionString)));

            services.AddScoped<IUnitOfWork<long>, UnitOfWork<long>>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<TableDbContext>();
                var unitOfWork = new UnitOfWork<long>(context);
                unitOfWork.RegisterRepositories(typeof(IRepository<,>).Assembly, typeof(Repository<,>).Assembly);
                return unitOfWork;
            });

            return services;
        }
    }
}
