using System;
using AutoMapper;
using Betting_Aggregator.API.Dtos;
using Betting_Aggregator.Business;
using Betting_Aggregator.Business.Database;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Betting_Aggregator.API
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
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddControllers(options => options.Filters.Add(new BadRequestFilter()))
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = context =>
                        {
                            var problems = new CustomBadRequest(context);

                            return new BadRequestObjectResult(problems);
                        };
                    });  

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(DbConnectionStringBuilder(services).ConnectionString));
            services.AddTransient<IHealthCheckService, HealthCheckService>();
            services.AddTransient<ILeagueService, LeagueService>();
        }

        private static NpgsqlConnectionStringBuilder DbConnectionStringBuilder(IServiceCollection services)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();

            var connectionString = config["PostgreSql:ConnectionString"];
            var dbPassword = config["PostgreSql:DbPassword"];

            var ngBuilder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };

            return ngBuilder;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCorrelationId();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
