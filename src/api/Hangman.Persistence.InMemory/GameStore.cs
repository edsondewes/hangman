using System;
using System.Collections.Concurrent;
using Hangman.Core;

namespace Hangman.Persistence.InMemory
{
    internal static class GameStore
    {
        public static ConcurrentDictionary<Guid, Game> Games { get; }

        static GameStore()
        {
            Games = new ConcurrentDictionary<Guid, Game>();
        }
    }
}
