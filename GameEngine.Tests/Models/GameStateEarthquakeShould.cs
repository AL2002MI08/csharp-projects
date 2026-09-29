using GameEngine.Constants;
using GameEngine.Models;
using GameEngine.Tests.Constants;
using GameEngine.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests.Models
{
    [Collection(GameStateCollection.Name)]
    [Trait(TestCategories.TraitName, TestCategories.GameState)]
    public class GameStateEarthquakeShould : IDisposable
    {
        private readonly GameState _sut;

        public GameStateEarthquakeShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _sut = gameStateFixture.State;
            output.WriteLine($"GameState ID={_sut.Id}");
        }

        public void Dispose()
        {
            _sut.Players.Clear();
        }

        [Fact]
        public void DamageAllPlayers()
        {
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();
            _sut.Players.Add(player1);
            _sut.Players.Add(player2);

            var expectedHealth = player1.Health - GameConstants.EarthquakeDamage;

            _sut.Earthquake();

            Assert.Equal(expectedHealth, player1.Health);
            Assert.Equal(expectedHealth, player2.Health);
        }
    }
}
