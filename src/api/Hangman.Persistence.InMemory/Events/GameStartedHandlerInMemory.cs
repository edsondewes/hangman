using System.Threading;
using System.Threading.Tasks;
using Hangman.Core.Events;
using MediatR;

namespace Hangman.Persistence.InMemory.Events
{
    public class GameStartedHandlerInMemory : INotificationHandler<GameStartedEvent>
    {
        public Task Handle(GameStartedEvent notification, CancellationToken cancellationToken)
        {
            GameStore.Games.TryAdd(notification.Game.Id, notification.Game);
            return Task.CompletedTask;
        }
    }
}
