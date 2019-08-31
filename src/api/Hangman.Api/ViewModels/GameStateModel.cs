using System;
using Hangman.Core;

namespace Hangman.Api.ViewModels
{
    public class GameStateModel
    {
        public Guid Id { get; set; }
        public char[] GuessedLetters { get; set; }
        public int HelpsLeft { get; set; }
        public int MaxWrongGuesses { get; set; }
        public Status Status { get; set; }
        public char[] Word { get; set; }
        public int WrongGuessesLeft { get; set; }

        public static GameStateModel Map(Game game)
        {
            return new GameStateModel
            {
                Id = game.Id,
                GuessedLetters = game.GuessedLetters(),
                HelpsLeft = game.HelpsLeft(),
                MaxWrongGuesses = game.Parameters.MaxWrongGuesses,
                Status = game.Status(),
                Word = game.VisibleLetters(),
                WrongGuessesLeft = game.WrongGuessesLeft(),
            };
        }
    }
}
