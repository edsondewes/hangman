using System;
using MediatR;

namespace Hangman.Core.Queries
{
    /// <summary>
    /// Try to find a game by its ID
    /// </summary>
    public class GameByIdQuery : IRequest<Game>
    {
        /// <summary>
        /// Game ID
        /// </summary>
        public Guid Id { get; }

        public GameByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
