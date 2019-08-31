using MediatR;

namespace Hangman.Core.Events
{
    /// <summary>
    /// Event fired when a move is applied to a game
    /// </summary>
    public class MoveAppliedEvent : INotification
    {
        public Game Game { get; }

        public MoveAppliedEvent(Game game)
        {
            Game = game;
        }
    }
}
