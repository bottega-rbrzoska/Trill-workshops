using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Trill.Api.Middlewares;
using Trill.Api.Services;
using Trill.Application;
using Trill.Application.Commands;
using Trill.Application.Services;
using Trill.Infrastructure;

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
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trill API",
                Version = "v1"
            }));
            services.AddApplication();
            services.AddInfrastructure();

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
            app.UseSwagger();
            app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Trill API"));

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
                endpoints.MapControllers();
                
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
                    var story = await context.RequestServices.GetRequiredService<IStoryService>().GetAsync(storyId);
                    if (story is null)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }

                    // var json = JsonSerializer.Serialize(story);
                    var json = JsonConvert.SerializeObject(story);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });

                endpoints.MapPost("stories", async context =>
                {
                    var body = context.Request.Body;
                    if (body is null)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        return;
                    }

                    var json = await new StreamReader(body).ReadToEndAsync();
                    var command = JsonConvert.DeserializeObject<SendStory>(json);
                    // var command = JsonSerializer.Deserialize<SendStory>(json, new JsonSerializerOptions
                    // {
                    //     PropertyNameCaseInsensitive = true
                    // });
                    await context.RequestServices.GetRequiredService<IStoryService>().AddAsync(command);
                    
                    context.Response.Headers.Add("Location", $"stories/{command.Id}");
                    context.Response.StatusCode = StatusCodes.Status201Created;
                });
            });
        }
    }
}