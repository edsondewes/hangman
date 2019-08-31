using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Hangman.Core.Commands
{
    public class GenerateWordHandler : IRequestHandler<GenerateWordCommand, string>
    {
        private static int _gameNumber = 0;
        private static readonly List<string> _words = new List<string>
        {
            "dell",
            "deliver",
            "technology",
            "customer",
            "cloud",
            "computer",
            "server",
            "advanced",
            "storage",
            "sollutions",
            "commitment",
            "diversity",
            "engagement",
            "community",
            "milestone",
        };

        public Task<string> Handle(GenerateWordCommand request, CancellationToken cancellationToken)
        {
            var index = _gameNumber < _words.Count
                ? _gameNumber
                : _gameNumber % _words.Count; // use the mod to reset the index

            var word = _words[index];
            Interlocked.Increment(ref _gameNumber);

            return Task.FromResult(word);
        }
    }
}
