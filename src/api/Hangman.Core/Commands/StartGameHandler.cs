using System;
using System.Threading;
using System.Threading.Tasks;
using Hangman.Core.Events;
using MediatR;

namespace Hangman.Core.Commands
{
    public class StartGameHandler : IRequestHandler<StartGameCommand, Game>
    {
        private readonly IMediator _mediator;

        public StartGameHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Game> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var word = await _mediator.Send(new GenerateWordCommand());
            var gameId = Guid.NewGuid();
            var game = new Game(gameId, request.Parameters, word);

            await _mediator.Publish(new GameStartedEvent(game));

            return game;
        }
    }
}
