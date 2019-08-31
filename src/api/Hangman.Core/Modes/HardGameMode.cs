namespace Hangman.Core.Modes
{
    /// <summary>
    /// Hard mode, no help, only one shot
    /// </summary>
    public class HardGameMode : IParameters
    {
        public int MaxHelpsAllowed => 0;
        public int MaxWrongGuesses => 1;
    }
}
