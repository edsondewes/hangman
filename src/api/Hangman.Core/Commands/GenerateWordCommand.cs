using MediatR;

namespace Hangman.Core.Commands
{
    /// <summary>
    /// Generate a random word
    /// </summary>
    public class GenerateWordCommand : IRequest<string>
    {
    }
}
