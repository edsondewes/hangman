using System;
using MediatR;

namespace Hangman.Core.Commands
{
    /// <summary>
    /// Try to apply a move to a game
    /// </summary>
    public class ApplyMoveCommand : IRequest<Game>
    {
        public Guid GameId { get; }
        public Move Move { get; }

        public ApplyMoveCommand(Guid gameId, Move move)
        {
            if (move is null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            GameId = gameId;
            Move = move;
        }
    }
}
