using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using REST_API_Lotus.Business.Implementation;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Serialization;
using REST_API_Lotus.Business;
using REST_API_Lotus.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace REST_API_Lotus
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            //JSON Serializer
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

            services.AddControllers();

            //Versioning API
            services.AddApiVersioning();

            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Rest API's from Lotus SPA - TCC",
                        Version = "v1",
                        Description = "API RESTful para desenvolvimento da empresa Lotus SPa, com ASP .NET CORE 5",
                        Contact = new OpenApiContact
                        {
                            Name = "Micael Rodrigues",
                            Url = new Uri("https://github.com/mresanto")
                        }
                    });
            });

            services.AddScoped<ICustomerBusiness, CustomerBusinessImplementation>();
            services.AddScoped<ICustomerRepository, CustomerRepositoryImplementation>();

            services.AddScoped<IProductBusiness, ProductBusinessImplementation>();
            services.AddScoped<IProductRepository, ProductRepositoryImplementation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>{
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "API RESTful para desenvolvimento da empresa Lotus SPa, com ASP .NET CORE 5");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
