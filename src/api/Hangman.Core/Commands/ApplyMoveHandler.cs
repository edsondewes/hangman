using System;
using System.Threading;
using System.Threading.Tasks;
using Hangman.Core.Events;
using Hangman.Core.Queries;
using MediatR;

namespace Hangman.Core.Commands
{
    public class ApplyMoveHandler : IRequestHandler<ApplyMoveCommand, Game>
    {
        private readonly IMediator _mediator;

        public ApplyMoveHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Game> Handle(ApplyMoveCommand request, CancellationToken cancellationToken)
        {
            var game = await _mediator.Send(new GameByIdQuery(request.GameId));
            if (game is null)
            {
                throw new ArgumentException("Game not found", nameof(request.GameId));
            }

            game.Apply(request.Move);
            await _mediator.Publish(new MoveAppliedEvent(game));

            return game;
        }
    }
}
