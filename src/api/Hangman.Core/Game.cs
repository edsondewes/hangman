using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman.Core
{
    /// <summary>
    /// Manager of a game
    /// </summary>
    public class Game
    {
        public Guid Id { get; }
        public IEnumerable<Move> Moves => _moves;
        public string Word { get; }
        public IParameters Parameters { get; }

        private GameState _currentState;
        private readonly List<Move> _moves;

        /// <summary>
        /// Create an instance of a game
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <param name="parameters">Configuration parameters</param>
        /// <param name="word">Word to be guessed</param>
        /// <param name="moves">List of moves applied to the game</param>
        public Game(Guid id, IParameters parameters, string word, IEnumerable<Move> moves = null)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            Id = id;
            Parameters = parameters;
            Word = word;

            _moves = moves?.ToList() ?? new List<Move>();
            _currentState = RecreateState();
        }

        /// <summary>
        /// Try to apply some move to the game and change the current state
        /// </summary>
        /// <param name="move">Move to be applied</param>
        public void Apply(Move move)
        {
            var status = Status();
            if (status == Core.Status.Lose || status == Core.Status.Win)
            {
                throw new InvalidOperationException("The game is over");
            }

            if (move is HelpMove && HelpsLeft() == 0)
            {
                throw new InvalidOperationException("No help available");
            }

            _moves.Add(move);
            _currentState = RecreateState();
        }

        /// <summary>
        /// Shows wich letters cannot be used anymore
        /// </summary>
        /// <returns></returns>
        public char[] GuessedLetters()
        {
            return _currentState.GuessedLetters;
        }

        /// <summary>
        /// Number of help moves available to the user
        /// </summary>
        /// <returns>Number</returns>
        public int HelpsLeft()
        {
            var helpsUsed = Moves.OfType<HelpMove>().Count();
            var helpsAvailable = Parameters.MaxHelpsAllowed - helpsUsed;

            return helpsAvailable;
        }

        /// <summary>
        /// Status of the game
        /// </summary>
        /// <returns>
        /// Lost = The user used all their chances
        /// Win = All letters are revealed
        /// Open = The game is still going
        /// </returns>
        public Status Status()
        {
            var wrongGuessesLeft = WrongGuessesLeft();
            if (wrongGuessesLeft == 0)
            {
                return Core.Status.Lose;
            }

            var visibleLetters = VisibleLetters();
            if (!visibleLetters.Any(letter => letter == default))
            {
                return Core.Status.Win;
            }

            return Core.Status.Open;
        }

        /// <summary>
        /// Show the word silhouette
        /// </summary>
        /// <returns>Char Array</returns>
        public char[] VisibleLetters()
        {
            var letters = _currentState.Word
                .Select(letter => _currentState.GuessedLetters.Contains(letter) ? letter : default)
                .ToArray();

            return letters;
        }

        /// <summary>
        /// Number of wrong guesses available to the user
        /// </summary>
        /// <returns>Number</returns>
        public int WrongGuessesLeft()
        {
            var numberOfGuesses = _currentState.GuessedLetters.Length;
            var numberOfRightGuesses = RightGuesses().Length;

            var numberOfWrongGuesses = numberOfGuesses - numberOfRightGuesses;
            var wrongGuessesLeft = Parameters.MaxWrongGuesses - numberOfWrongGuesses;

            return wrongGuessesLeft;
        }

        private GameState RecreateState()
        {
            var finalState = Moves.Aggregate(
                seed: new GameState(Word),
                func: (state, command) => command.Apply(state)
                );

            return finalState;
        }

        private char[] RightGuesses()
        {
            var letters = _currentState.GuessedLetters
                .Where(letter => _currentState.Word.Contains(letter))
                .ToArray();

            return letters;
        }
    }
}
