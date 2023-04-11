using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

using TableTracker.Infrastructure;
using TableTracker.Infrastructure.Identity;

namespace TableTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Migrate(host.Services);
            host.Run();
        }

        public static void Migrate(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TableDbContext>();
            var identityDbContext = scope.ServiceProvider.GetRequiredService<IdentityTableDbContext>();

            var seed = new DataSeed(
                scope.ServiceProvider
                    .GetRequiredService<UserManager<TableTrackerIdentityUser>>(),
                scope.ServiceProvider
                    .GetRequiredService<RoleManager<TableTrackerIdentityRole>>());

            seed.SeedData(dbContext, identityDbContext).Wait();

            dbContext.Database.Migrate();
            identityDbContext.Database.Migrate();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}
