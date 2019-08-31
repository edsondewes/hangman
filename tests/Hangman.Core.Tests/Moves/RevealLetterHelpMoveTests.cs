using Hangman.Core.Moves;
using Xunit;

namespace Hangman.Core.Tests.Moves
{
    public class RevealLetterHelpMoveTests
    {
        [Fact]
        public void ApplyShouldAppendTheFirstNonGuessedLetter()
        {
            var state = new GameState("word", new[] { 'd', 'w', 'x' });
            var move = new RevealLetterHelpMove();
            var newState = move.Apply(state);

            Assert.Equal(new[] { 'd', 'w', 'x', 'o' }, newState.GuessedLetters);
        }

        [Fact]
        public void ApplyShouldNotMutateTheOldState()
        {
            var state = new GameState("word");
            var move = new RevealLetterHelpMove();
            var newState = move.Apply(state);

            Assert.NotSame(newState, state);
            Assert.NotSame(newState.GuessedLetters, state.GuessedLetters);
            Assert.Empty(state.GuessedLetters);
            Assert.Single(newState.GuessedLetters);
        }
    }
}
