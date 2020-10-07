using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Trill.Api.Services;

namespace Trill.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMessenger, Messenger>();
            services.Configure<ApiOptions>(_configuration.GetSection("api"));
            services.AddHostedService<NotificationsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var options = context.RequestServices.GetRequiredService<IOptions<ApiOptions>>();
                    var name = options.Value.Name;
                    
                    await context.Response.WriteAsync(name);
                });

                endpoints.MapGet("stories/{storyId:guid}", async context =>
                {
                    var storyId = Guid.Parse(context.Request.RouteValues["storyId"].ToString());
                    if (storyId == Guid.Empty)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{}");
                });

                endpoints.MapPost("stories", async context =>
                {
                    var storyId = Guid.NewGuid();
                    // Save story to DB
                    context.Response.Headers.Add("Location", $"stories/{storyId}");
                    context.Response.StatusCode = StatusCodes.Status201Created;
                });
            });
        }
    }
}