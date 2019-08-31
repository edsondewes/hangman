using System.ComponentModel.DataAnnotations;

namespace Hangman.Api.ViewModels
{
    public class GuessLetterModel
    {
        [RegularExpression("[a-z]", ErrorMessage = "Only lowercase letters are allowed")]
        public char Letter { get; set; }
    }
}
