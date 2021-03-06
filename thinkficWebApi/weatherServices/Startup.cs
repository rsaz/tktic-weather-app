using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weatherServices.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using weatherServices.Filters;

namespace weatherServices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Using in memory database to store the data from the weather api : dev and testing
            services.AddDbContext<WeatherApiDbContext>(options =>
                options.UseInMemoryDatabase("weatherdb")
            );

            services.AddControllers();

            // Adding Api documentation service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{ApiVersion.Default.MajorVersion}", new OpenApiInfo { Title = "WeatherServices", Version = $"v1{ApiVersion.Default.MajorVersion}" });
                c.AddSecurityDefinition("Auth", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Query,
                    Name = "Auth",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Auth",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Query,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey",
                            },
                        },
                        new string[]{ }
                    }
                });
            });

            // Adding the api versioning service
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddCors(options =>
            {
                // During development allow any origin
                // In prod change to be a specific origing requester
                options.AddPolicy("AllowMyApp", policy => policy.AllowAnyOrigin());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{ApiVersion.Default.MajorVersion}/swagger.json", "WeatherServices v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{ApiVersion.Default.MajorVersion}/swagger.json", "WeatherServices v1"));

                app.ConfigureExceptionHandler();
                app.UseHsts();
            }

            app.UseCors("AllowMyApp");

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
