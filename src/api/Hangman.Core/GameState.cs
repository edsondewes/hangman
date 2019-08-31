namespace Hangman.Core
{
    /// <summary>
    /// Current state of the game
    /// This state should ony be manipulated by moves applied to the game
    /// </summary>
    public class GameState
    {
        public char[] GuessedLetters { get; }
        public string Word { get; }

        public GameState(string word, char[] guessedLetters = null)
        {
            GuessedLetters = guessedLetters ?? new char[0];
            Word = word;
        }
    }
}
