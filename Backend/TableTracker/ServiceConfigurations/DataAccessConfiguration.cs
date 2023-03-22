using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContext<TableDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TableTracker")));

            services.AddDbContext<IdentityTableDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityTableTracker")));

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
