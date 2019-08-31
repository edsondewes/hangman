using Hangman.Core;
using Hangman.Core.Modes;
using Hangman.Core.Moves;
using MongoDB.Bson.Serialization;

namespace Hangman.Persistence.MongoDb
{
    public static class MappingMongoDb
    {
        public static void MapEntities()
        {
            BsonClassMap.RegisterClassMap<Game>(cm =>
            {
                cm.MapIdMember(g => g.Id);
                cm.MapMember(g => g.Moves).SetElementName("m");
                cm.MapMember(g => g.Parameters).SetElementName("p");
                cm.MapMember(g => g.Word).SetElementName("w");

                cm.MapCreator(g => new Game(g.Id, g.Parameters, g.Word, g.Moves));
            });

            BsonClassMap.RegisterClassMap<EasyGameMode>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<HardGameMode>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<GuessLetterMove>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(g => g.Letter).SetElementName("l");
            });

            BsonClassMap.RegisterClassMap<RevealLetterHelpMove>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
