using System;
using ContentAuthorizator.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Middlewares;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace ContentAuthorizator
{
    public class Startup
    {
        public Startup()
        {
            var config = new Configuration();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config.ElasticSearchURL))
                {
                    AutoRegisterTemplate = true
                })
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            }));

            services.AddSingleton<IAuthorizationRepository>(new AuthorizationRepository());
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
