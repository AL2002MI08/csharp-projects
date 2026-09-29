using GameEngine.Models;
using GameEngine.Tests.Constants;
using GameEngine.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests.Models
{
    [Collection(GameStateCollection.Name)]
    [Trait(TestCategories.TraitName, TestCategories.GameState)]
    public class GameStateResetShould
    {
        private readonly GameState _sut;

        public GameStateResetShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _sut = gameStateFixture.State;
            output.WriteLine($"GameState ID={_sut.Id}");
        }

        [Fact]
        public void RemoveAllPlayers()
        {
            _sut.Players.Add(new PlayerCharacter());
            _sut.Players.Add(new PlayerCharacter());

            _sut.Reset();

            Assert.Empty(_sut.Players);
        }
    }
}
