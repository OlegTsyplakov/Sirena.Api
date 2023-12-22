using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Sirena.Api.Domain.Services;
using Sirena.Api.Services;
using Sirena.Api.Validation;
using System;
using System.Net.Http;

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
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy()); 


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

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
