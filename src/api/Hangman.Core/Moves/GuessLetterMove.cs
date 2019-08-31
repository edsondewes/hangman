using System;
using System.Linq;

namespace Hangman.Core.Moves
{
    /// <summary>
    /// Apply a letter to the guessed letters of a game
    /// </summary>
    public class GuessLetterMove : Move
    {
        public char Letter { get; }

        public GuessLetterMove(char letter)
        {
            Letter = letter;
        }

        public override GameState Apply(GameState state)
        {
            if (state.GuessedLetters.Contains(Letter))
            {
                throw new InvalidOperationException("This letter has already been used");
            }

            var newGuessedLetters = state.GuessedLetters.CopyAndAppend(Letter);
            return new GameState(state.Word, newGuessedLetters);
        }
    }
}
