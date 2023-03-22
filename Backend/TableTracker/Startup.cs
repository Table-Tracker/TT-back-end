using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Settings;
using TableTracker.Infrastructure;
using TableTracker.ServiceConfigurations;

namespace TableTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger();
            services.AddMapper();
            services.AddDataAccess(Configuration);
            services.AddMediator();
            services.AddValidaton();
            services.AddCustomAuthorization(Configuration);
            services.AddApiControllers();

            services.AddScoped<IEmailHandler, EmailHandler>();
            services.AddOptions();
            services.Configure<EmailConfig>(Configuration.GetSection(nameof(EmailConfig)));
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = Int32.MaxValue;
                x.MultipartBodyLengthLimit = Int32.MaxValue;
                x.MemoryBufferThreshold = Int32.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TableTracker v1"));
            }

            app.UseStaticFiles();
            app.UseCustomMiddlewares();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCrossOriginResourceSharing();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
