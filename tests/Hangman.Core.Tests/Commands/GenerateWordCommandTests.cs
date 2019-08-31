using System.Threading.Tasks;
using Hangman.Core.Commands;
using Xunit;

namespace Hangman.Core.Tests.Commands
{
    public class GenerateWordCommandTests
    {
        [Fact]
        public async Task HandlerShouldGenerateADifferentWordFromThePreviousOne()
        {
            string previousWord = null;
            var handler = new GenerateWordHandler();
            for (var i = 0; i < 20; i++)
            {
                var newWord = await handler.Handle(new GenerateWordCommand(), default);
                Assert.NotEqual(previousWord, newWord);

                previousWord = newWord;
            }
        }
    }
}
