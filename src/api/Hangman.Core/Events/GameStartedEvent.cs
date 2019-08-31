using MediatR;

namespace Hangman.Core.Events
{
    /// <summary>
    /// Event fired whe a new game starts
    /// </summary>
    public class GameStartedEvent : INotification
    {
        public Game Game { get; }

        public GameStartedEvent(Game game)
        {
            Game = game;
        }
    }
}
