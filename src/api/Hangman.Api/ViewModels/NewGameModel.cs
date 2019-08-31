using System.ComponentModel.DataAnnotations;

namespace Hangman.Api.ViewModels
{
    public class NewGameModel
    {
        [EnumDataType(typeof(GameModeKind))]
        public GameModeKind Kind { get; set; }

        public enum GameModeKind
        {
            Easy = 0,
            Hard = 1,
        }
    }
}
