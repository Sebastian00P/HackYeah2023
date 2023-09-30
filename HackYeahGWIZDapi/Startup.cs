using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.AppModule;
using HackYeahGWIZDapi.AppServices;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHangfireServer();
            services.AddHangfire(configuration => configuration
              .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEventApplicationService, EventApplicationService>();
            services.AddScoped<IAnimalApplicationService, AnimalApplicationService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HackYeahGWIZDapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, IJobApplicationService jobApplicationService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HackYeahGWIZDapi v1"));
            }

            app.UseHttpsRedirection();
            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => Console.WriteLine("Hangfire!"));
            RecurringJob.AddOrUpdate(() => jobApplicationService.CheckMultiplyEvents(), Cron.MinuteInterval(1));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
