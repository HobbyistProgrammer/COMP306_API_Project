using Amazon.DynamoDBv2;
using AutoMapper;
using COMP306_API_Demo.Mappings;
using COMP306_API_Demo.Models;
using COMP306_API_Demo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo
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
            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "COMP306 API",
                    Version = "v1"
                });
            });

            // Register the Amazon DynamoDB client as a singleton
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var config = new AmazonDynamoDBConfig
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast2 // Set your region
                };
                return new AmazonDynamoDBClient(config);
            });

            // Register ProductContext with DynamoDB
            services.AddSingleton<ProductContext>();

            // Register other services and repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enables middleware to use generated Swagger as a JSON endpoint
                app.UseSwagger();

                // Enable middleware to server swagger-ui
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
