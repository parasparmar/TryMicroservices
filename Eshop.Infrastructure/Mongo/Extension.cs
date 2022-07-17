using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Mongo
{
    public static class Extension
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("mongo");
            var mongoConfig = new MongoConfig();
            configSection.Bind(mongoConfig);
            // Replace <connection string> with your MongoDB deployment's connection string.
            var mongoClient = new MongoClient(mongoConfig.ConnectionString);
            services.AddSingleton<IMongoClient>(client =>
            {
                //Keeps a single Mongo client throughout the entire applications and all Microservices.
                return mongoClient;
            });
            services.AddSingleton<IMongoDatabase>(client =>
            {
                
                return mongoClient.GetDatabase(mongoConfig.Database);
            });

            services.AddSingleton<IDatabaseInitializer, MongoInitializer>();
        }
    }
}
