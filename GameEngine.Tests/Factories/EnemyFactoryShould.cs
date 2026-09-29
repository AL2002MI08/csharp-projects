using GameEngine.Exceptions;
using GameEngine.Factories;
using GameEngine.Models;
using GameEngine.Tests.Constants;
using Xunit;

namespace GameEngine.Tests.Factories
{
    [Trait(TestCategories.TraitName, TestCategories.Enemy)]
    public class EnemyFactoryShould
    {
        private const string NormalEnemyName = "Zombie";
        private const string BossEnemyName = "Zombie King";

        private readonly EnemyFactory _sut;

        public EnemyFactoryShould()
        {
            _sut = new EnemyFactory();
        }

        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            Enemy enemy = _sut.Create(NormalEnemyName);

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact(Skip = "Don't need to run this")]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            Enemy enemy = _sut.Create(NormalEnemyName);

            Assert.IsNotType<DateTime>(enemy);
        }

        [Theory]
        [InlineData("Zombie King")]
        [InlineData("Zombie Queen")]
        public void CreateBossEnemy(string bossName)
        {
            Enemy enemy = _sut.Create(bossName, true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            Enemy enemy = _sut.Create(BossEnemyName, true);

            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            Assert.Equal(BossEnemyName, boss.Name);
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            Enemy enemy = _sut.Create(BossEnemyName, true);

            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            Enemy enemy1 = _sut.Create(NormalEnemyName);
            Enemy enemy2 = _sut.Create(NormalEnemyName);

            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            Assert.Throws<ArgumentNullException>("name", () => _sut.Create(null!));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            EnemyCreationException ex =
                Assert.Throws<EnemyCreationException>(() => _sut.Create(NormalEnemyName, true));

            Assert.Equal(NormalEnemyName, ex.RequestedEnemyName);
        }
    }
}
