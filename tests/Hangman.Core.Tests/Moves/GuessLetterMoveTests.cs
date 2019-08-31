using System;
using Hangman.Core.Moves;
using Xunit;

namespace Hangman.Core.Tests.Moves
{
    public class GuessLetterMoveTests
    {
        [Fact]
        public void ApplyShouldAddTheLetterToTheState()
        {
            var state = new GameState("word");
            var move = new GuessLetterMove('a');
            var newState = move.Apply(state);

            Assert.Equal(new[] { 'a' }, newState.GuessedLetters);
        }

        [Fact]
        public void ApplyShouldNotAllowAnExistingLetter()
        {
            var state = new GameState("word", new[] { 'm', 'w', 'o' });
            var move = new GuessLetterMove('w');

            Assert.Throws<InvalidOperationException>(() => move.Apply(state));
        }

        [Fact]
        public void ApplyShouldNotMutateTheOldState()
        {
            var state = new GameState("word");
            var move = new GuessLetterMove('a');
            var newState = move.Apply(state);

            Assert.NotSame(newState, state);
            Assert.NotSame(newState.GuessedLetters, state.GuessedLetters);
            Assert.Empty(state.GuessedLetters);
            Assert.Single(newState.GuessedLetters);
        }
    }
}
