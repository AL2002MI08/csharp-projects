using Xunit;

namespace GameEngine.Tests.Fixtures
{
    [CollectionDefinition(Name)]
    public class GameStateCollection : ICollectionFixture<GameStateFixture>
    {
        public const string Name = "GameState collection";
    }
}
