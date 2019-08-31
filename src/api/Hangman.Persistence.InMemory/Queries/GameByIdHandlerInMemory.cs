using System.Threading;
using System.Threading.Tasks;
using Hangman.Core;
using Hangman.Core.Queries;
using MediatR;

namespace Hangman.Persistence.InMemory.Queries
{
    public class GameByIdHandlerInMemory : IRequestHandler<GameByIdQuery, Game>
    {
        public Task<Game> Handle(GameByIdQuery request, CancellationToken cancellationToken)
        {
            GameStore.Games.TryGetValue(request.Id, out var game);
            return Task.FromResult(game);
        }
    }
}
