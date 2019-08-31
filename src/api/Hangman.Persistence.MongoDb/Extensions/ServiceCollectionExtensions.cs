using Hangman.Core;
using Hangman.Persistence.MongoDb;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            MappingMongoDb.MapEntities();

            // Database instance
            services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<ConfigMongoDb>>();

                var client = new MongoClient(config.Value.Host);
                var db = client.GetDatabase(config.Value.Database);

                return db;
            });

            // Game collection instance
            services.AddSingleton(provider =>
            {
                var db = provider.GetRequiredService<IMongoDatabase>();
                var collection = db.GetCollection<Game>("game");

                return collection;
            });

            services.AddMediatR(typeof(ConfigMongoDb));

            return services;
        }
    }
}
