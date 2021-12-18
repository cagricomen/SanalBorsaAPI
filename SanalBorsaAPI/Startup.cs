using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using SanalBorsaAPI.Core.Repositories;
using SanalBorsaAPI.Core.Services;
using SanalBorsaAPI.Core.UnitOfWorks;
using SanalBorsaAPI.Data;
using SanalBorsaAPI.Data.Repositories;
using SanalBorsaAPI.Data.UnitOfWorks;
using SanalBorsaAPI.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanalBorsaAPI
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("SanalBorsaAPI.Data");
                });
            });
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var goldKey = new JobKey("Gold Key");
                var crypthoKey = new JobKey("Cryptho Key");
                var stocksKey = new JobKey("Stocks Key");
                var exChangeKey = new JobKey("Exchange Key");
                q.AddJob<QuartzCryptho>(opts => opts.WithIdentity(goldKey));
                q.AddJob<QuartzGolden>(opts => opts.WithIdentity(crypthoKey));
                q.AddJob<QuartzStocks>(opts => opts.WithIdentity(stocksKey));
                q.AddJob<QuartzExChange>(opts => opts.WithIdentity(exChangeKey));
                q.AddTrigger(opts => opts
                   .ForJob(crypthoKey)
                   .WithIdentity("Cryptho Key Trigger")
                   .WithCronSchedule("0 0/3 * * * ?")); //every day every 3 minute
                q.AddTrigger(opts => opts
                  .ForJob(exChangeKey)
                  .WithIdentity("ExChangeRates Key Trigger")
                  .WithCronSchedule("0 0/5 * * * ?")); //every day every 5 minute
                q.AddTrigger(opts => opts
                    .ForJob(goldKey)
                    .WithIdentity("Gold Key Trigger")
                    .WithCronSchedule("0 0/7 * * * ?")); //every day every 7 minute
                q.AddTrigger(opts => opts
                    .ForJob(stocksKey)
                    .WithIdentity("Stocks Key Trigger")
                    .WithCronSchedule("0 0/9 * * * ?")); //every day every 9 minute
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SanalBorsaAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SanalBorsaAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
