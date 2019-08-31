namespace Hangman.Core
{
    /// <summary>
    /// Configuration used by a game
    /// </summary>
    public interface IParameters
    {
        int MaxHelpsAllowed { get; }
        int MaxWrongGuesses { get; }
    }
}
