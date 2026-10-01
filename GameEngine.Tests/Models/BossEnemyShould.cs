using GameEngine.Models;
using GameEngine.Tests.Constants;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests.Models
{
    [Trait(TestCategories.TraitName, TestCategories.Enemy)]
    public class BossEnemyShould
    {
        private readonly BossEnemy _sut;
        private readonly ITestOutputHelper _output;

        public BossEnemyShould(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Creating Boss Enemy...");
            _sut = new BossEnemy();
        }

        [Fact]
        public void HaveCorrectPower()
        {
            Assert.Equal(166.667, _sut.SpecialAttackPower, 3);
        }
    }
}
