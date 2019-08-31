using System;
using System.Linq;
using Hangman.Core.Moves;
using Xunit;

namespace Hangman.Core.Tests.Modes
{
    public class GameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]

        public void ConstructorShouldNotAllowNullOrEmptyWord(string word)
        {
            Assert.Throws<ArgumentNullException>(() => new Game(Guid.Empty, new FakeParameters(), word));
        }

        [Fact]
        public void ConstructorShouldNotAllowNullOrNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Game(Guid.Empty, null, "word"));
        }

        [Fact]
        public void ApplyShouldNotAllowExtraHelps()
        {
            var parameters = new FakeParameters();
            var game = new Game(Guid.Empty, parameters, "word");
            for (var helpsUsed = 0; helpsUsed <= parameters.MaxHelpsAllowed; helpsUsed++)
            {
                if (helpsUsed == parameters.MaxHelpsAllowed)
                {
                    Assert.Throws<InvalidOperationException>(() => game.Apply(new FakeHelpMove()));
                }
                else
                {
                    game.Apply(new FakeHelpMove());
                }
            }
        }

        [Fact]
        public void ApplyShouldNotAllowNewMovesWhenTheGameIsLost()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "word");
            game.Apply(new GuessLetterMove('a'));
            game.Apply(new GuessLetterMove('b'));
            game.Apply(new GuessLetterMove('c'));
            Assert.Throws<InvalidOperationException>(() => game.Apply(new GuessLetterMove('e')));
        }

        [Fact]
        public void ApplyShouldNotAllowNewMovesWhenTheGameIsWon()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "word");
            game.Apply(new GuessLetterMove('w'));
            game.Apply(new GuessLetterMove('o'));
            game.Apply(new GuessLetterMove('r'));
            game.Apply(new GuessLetterMove('d'));
            Assert.Throws<InvalidOperationException>(() => game.Apply(new GuessLetterMove('x')));
        }

        [Fact]
        public void GuessedLettersShouldReturnTheCurrentState()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "word");
            game.Apply(new GuessLetterMove('a'));
            game.Apply(new GuessLetterMove('b'));

            Assert.Equal(new[] { 'a', 'b' }, game.GuessedLetters());
        }
        
        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        public void HelpsLeftShouldConsiderOnlyHelpMoves(int helpsApplied, int helpsLeftExpected)
        {
            var helpMoves = Enumerable.Range(0, helpsApplied).Select(_ => new FakeHelpMove());
            var game = new Game(Guid.Empty, new FakeParameters(), "myword", helpMoves);
            game.Apply(new GuessLetterMove('m'));
            game.Apply(new GuessLetterMove('y'));

            Assert.Equal(helpsLeftExpected, game.HelpsLeft());
        }

        [Theory]
        [InlineData("word", "word", Status.Win)]
        [InlineData("word", "abc", Status.Lose)]
        [InlineData("word", "ab", Status.Open)]
        [InlineData("word", "wor", Status.Open)]
        [InlineData("dell", "del", Status.Win)]
        [InlineData("community", "comunity", Status.Win)]
        public void StatusShouldMatchExpectation(string word, string letters, Status expectedStatus)
        {
            var game = new Game(Guid.Empty, new FakeParameters(), word);
            foreach (var letter in letters)
            {
                game.Apply(new GuessLetterMove(letter));
            }

            Assert.Equal(expectedStatus, game.Status());
        }

        [Fact]
        public void VisibleLettersShouldShowEmptyWithoutGuesses()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "myword");
            var visibleLetters = game.VisibleLetters();

            Assert.Equal(new char[] { default, default, default, default, default, default }, visibleLetters);
        }

        [Fact]
        public void VisibleLettersShouldShowTheRightGuesses()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "myword", new[]
            {
                new GuessLetterMove('x'),
                new GuessLetterMove('a'),
                new GuessLetterMove('w'),
                new GuessLetterMove('r'),
                new GuessLetterMove('y'),
            });

            var visibleLetters = game.VisibleLetters();

            Assert.Equal(new char[] { default, 'y', 'w', default, 'r', default }, visibleLetters);
        }

        [Fact]
        public void WrongGuessesLeftShouldConsiderOnlyWrongGuesses()
        {
            var game = new Game(Guid.Empty, new FakeParameters(), "myword");
            game.Apply(new GuessLetterMove('m'));
            game.Apply(new GuessLetterMove('y'));
            game.Apply(new GuessLetterMove('x'));
            game.Apply(new FakeHelpMove());
            game.Apply(new FakeHelpMove());
            game.Apply(new GuessLetterMove('j'));

            Assert.Equal(1, game.WrongGuessesLeft());
        }

        private class FakeParameters : IParameters
        {
            public int MaxHelpsAllowed => 2;
            public int MaxWrongGuesses => 3;
        }

        private class FakeHelpMove : HelpMove
        {
            public override GameState Apply(GameState state)
            {
                // do nothing
                return state;
            }
        }
    }
}
