using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Mongo
{
    public static class Extension
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("mongo");
            var mongoConfig = new MongoConfig();
            configSection.Bind(mongoConfig);

            services.AddSingleton<IMongoClient>(client =>
            {
                //Keeps a single Mongo client throughout the entire applications and all Microservices.
                return new MongoClient(mongoConfig.ConnectionString);
            });
            services.AddSingleton<IMongoDatabase>(client =>
            {
                var mongoClient = client.GetService<MongoClient>();
                return mongoClient.GetDatabase(mongoConfig.Database);
            });

            services.AddSingleton<IDatabaseInitializer, MongoInitializer>();
        }
    }
}
