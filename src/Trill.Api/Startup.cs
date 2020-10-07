using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Trill.Api.Middlewares;
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
            services.AddScoped<DummyMiddleware>();
            services.AddScoped<ErrorHandlerMiddleware>();

            // services.AddHostedService<NotificationsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("I'm the first middleware");
                await next();
            });
            
            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("I'm the second middleware");
                await next();
            });

            app.UseMiddleware<DummyMiddleware>();
            
            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Query.TryGetValue("token", out var token) && token == "secret")
                {
                    await ctx.Response.WriteAsync("Secret");
                    return;
                }

                await next();
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var options = context.RequestServices.GetRequiredService<IOptions<ApiOptions>>();
                    var name = options.Value.Name;
                    
                    await context.Response.WriteAsync(name);
                });

                endpoints.MapGet("/error", async context =>
                {
                    throw new ArgumentException("Ooppsss");
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