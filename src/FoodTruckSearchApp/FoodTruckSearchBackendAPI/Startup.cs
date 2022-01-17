using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckBingMapClient;
using FoodTruckSearchSodaClient;
using HttpClientWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FoodTruckSearchBackendAPI
{
    public class Startup
    {
        public string allowCORSOrigins = "allowCORSOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddScoped<IFoodTruckSearchSodaClient, FoodTruckSearchSodaClient.FoodTruckSearchSodaClient>();
            //services.AddScoped<IFoodTruckBingMapClient, FoodTruckBingMapClient.FoodTruckBingMapClient>();
            services.AddSingleton<IHttpClientWrapperClient, HttpClientWrapperClient>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Food Truck Search API",
                    Description = "An ASP.NET Core Web API for searching food trucks in San Francisco area based on search text, latitude and longitude.",

                });
            });

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: allowCORSOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:44364")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();;
                                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(allowCORSOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireCors(allowCORSOrigins); 
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

      
        }
    }
}
