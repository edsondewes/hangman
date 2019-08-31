namespace Hangman.Core.Modes
{
    /// <summary>
    /// Easy mode, lots of helps and attempts
    /// </summary>
    public class EasyGameMode : IParameters
    {
        public int MaxHelpsAllowed => 2;
        public int MaxWrongGuesses => 3;
    }
}
