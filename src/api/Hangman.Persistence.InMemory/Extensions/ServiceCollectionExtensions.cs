using Hangman.Persistence.InMemory;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryDb(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GameStore));

            return services;
        }
    }
}
