using System.Threading;
using System.Threading.Tasks;
using Hangman.Core;
using Hangman.Core.Queries;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Hangman.Persistence.MongoDb.Queries
{
    public class GameByIdHandlerMongoDb : IRequestHandler<GameByIdQuery, Game>
    {
        private readonly IMongoCollection<Game> _collection;

        public GameByIdHandlerMongoDb(IMongoCollection<Game> collection)
        {
            _collection = collection;
        }

        public Task<Game> Handle(GameByIdQuery request, CancellationToken cancellationToken)
        {
            return _collection
                .AsQueryable()
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
