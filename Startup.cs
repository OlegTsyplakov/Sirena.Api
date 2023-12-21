using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJsonSchema.Generation;
using NSwag.AspNetCore;
using Sirena.Api.Domain.Services;
using Sirena.Api.Services;
using Sirena.Api.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sirena.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOpenApiDocument(c =>
            {
                c.Title = "Sirena API";
                c.Version = "1.0";
                c.DocumentName = "v1";
                c.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                };


            });
            services.AddSingleton<ICacheService>(cache => new CacheService(Configuration.GetValue<int>("CacheLimit")));
            services.AddHttpClient<IRequestService, RequestService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("Url"));
            }); 
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
         
        }
     

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseMvc();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
