using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Trill.Core.Repositories;
using Trill.Infrastructure.Mongo.Repositories;

namespace Trill.Infrastructure.Mongo
{
    internal static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetRequiredService<IConfiguration>();
            }

            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>().Value;
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>().Value;
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });

            services.AddScoped<IStoryRepository, MongoStoryRepository>();

            ConventionRegistry.Register("trill", new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            }, _ => true);
            
            return services;
        }
    }
}