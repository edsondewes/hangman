namespace Hangman.Core
{
    public abstract class Move
    {
        public abstract GameState Apply(GameState state);
    }
}
