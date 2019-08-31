using System.Threading;
using System.Threading.Tasks;
using Hangman.Core;
using Hangman.Core.Events;
using MediatR;
using MongoDB.Driver;

namespace Hangman.Persistence.MongoDb.Events
{
    public class MoveAppliedHandlerMongoDb : INotificationHandler<MoveAppliedEvent>
    {
        private readonly IMongoCollection<Game> _collection;
        private readonly UpdateOptions _options;

        public MoveAppliedHandlerMongoDb(IMongoCollection<Game> collection)
        {
            _collection = collection;
            _options = new UpdateOptions { IsUpsert = false };
        }

        public Task Handle(MoveAppliedEvent notification, CancellationToken cancellationToken)
        {
            var filter = Builders<Game>.Filter.Eq(g => g.Id, notification.Game.Id);
            return _collection.ReplaceOneAsync(
                filter,
                notification.Game,
                _options,
                cancellationToken
                );
        }
    }
}
