using System;
using MediatR;

namespace Hangman.Core.Commands
{
    /// <summary>
    /// Starts a new game
    /// </summary>
    public class StartGameCommand : IRequest<Game>
    {
        public IParameters Parameters { get; }

        public StartGameCommand(IParameters parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            Parameters = parameters;
        }
    }
}
