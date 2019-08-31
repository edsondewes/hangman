using System.Threading;
using System.Threading.Tasks;
using Hangman.Core.Events;
using MediatR;

namespace Hangman.Persistence.InMemory.Events
{
    public class MoveAppliedHandlerInMemory : INotificationHandler<MoveAppliedEvent>
    {
        public Task Handle(MoveAppliedEvent notification, CancellationToken cancellationToken)
        {
            var store = GameStore.Games;
            if (store.TryGetValue(notification.Game.Id, out var oldGame))
            {
                store.TryUpdate(notification.Game.Id, notification.Game, oldGame);
            }
            
            return Task.CompletedTask;
        }
    }
}
