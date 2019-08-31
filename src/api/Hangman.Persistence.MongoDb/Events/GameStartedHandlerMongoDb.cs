using System.Threading;
using System.Threading.Tasks;
using Hangman.Core;
using Hangman.Core.Events;
using MediatR;
using MongoDB.Driver;

namespace Hangman.Persistence.MongoDb.Events
{
    public class GameStartedHandlerMongoDb : INotificationHandler<GameStartedEvent>
    {
        private readonly IMongoCollection<Game> _collection;
        private readonly InsertOneOptions _options;

        public GameStartedHandlerMongoDb(IMongoCollection<Game> collection)
        {
            _collection = collection;
            _options = new InsertOneOptions { BypassDocumentValidation = false };
        }

        public Task Handle(GameStartedEvent notification, CancellationToken cancellationToken)
        {
            return _collection.InsertOneAsync(
                notification.Game,
                _options,
                cancellationToken
                );
        }
    }
}
