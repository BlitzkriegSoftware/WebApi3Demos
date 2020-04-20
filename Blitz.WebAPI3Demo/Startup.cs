using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Blitz.WebAPI3Demo
{
    /// <summary>
    /// Startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        // Swagger groups

        /// <summary>
        /// Image Group
        /// </summary>
        public const string Images_Title = "images";
        /// <summary>
        /// Common Group
        /// </summary>
        public const string Common_Title = "common";

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(logger =>
            {
                logger.SetMinimumLevel(LogLevel.Debug);
                logger.AddConsole();
                logger.AddDebug();
            });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc(Images_Title, this.MakeOpenApiInfo(Images_Title, Program.ProgramMetadata.MajorVersion, $"{Images_Title} Demos", new Uri(Program.ProgramMetadata.GitHubUrl)));
                c.SwaggerDoc(Common_Title, this.MakeOpenApiInfo(Common_Title, Program.ProgramMetadata.MajorVersion, $"{Images_Title} Demos", new Uri(Program.ProgramMetadata.GitHubUrl)));

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.json";
                xmlfile = System.IO.Path.Combine(AppContext.BaseDirectory, xmlfile);
                if(System.IO.File.Exists(xmlfile))
                {
                    c.IncludeXmlComments(xmlfile);
                }


            });

        }

        private OpenApiInfo MakeOpenApiInfo(string title, string version, string description, Uri repo)
        {
            return new OpenApiInfo()
            {
                Title = title,
                Contact = new OpenApiContact()
                {
                    Email = "stuart.t.williams@outlook.com",
                    Name = "Stuart Williams",
                    Url = repo
                },
                Description = description,
                License = new OpenApiLicense()
                {
                    Name = "MIT"
                },
                Version = version
            };
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint($"/swagger/{Images_Title}/swagger.json", $"{Images_Title} {Program.ProgramMetadata.MajorVersion}");
                c.SwaggerEndpoint($"/swagger/{Common_Title}/swagger.json", $"{Common_Title} {Program.ProgramMetadata.MajorVersion}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
