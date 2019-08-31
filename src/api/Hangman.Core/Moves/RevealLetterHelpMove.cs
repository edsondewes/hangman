using System;
using System.Linq;

namespace Hangman.Core.Moves
{
    /// <summary>
    /// Help command that reveal the first letter not yet discovered
    /// </summary>
    public class RevealLetterHelpMove : HelpMove
    {
        public override GameState Apply(GameState state)
        {
            var firstNonGuessedLetter = state.Word
                .Where(letter => !state.GuessedLetters.Contains(letter))
                .First();

            var newGuessedLetters = state.GuessedLetters.CopyAndAppend(firstNonGuessedLetter);
            return new GameState(state.Word, newGuessedLetters);
        }
    }
}
