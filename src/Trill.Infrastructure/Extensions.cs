using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Trill.Core.Repositories;
using Trill.Infrastructure.Caching;
using Trill.Infrastructure.Middlewares;
using Trill.Infrastructure.Mongo;

namespace Trill.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IStoryRepository, InMemoryStoryRepository>();
            services.AddScoped<DummyMiddleware>();
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddMongo();
            
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<DummyMiddleware>();

            return app;
        }
    }
}