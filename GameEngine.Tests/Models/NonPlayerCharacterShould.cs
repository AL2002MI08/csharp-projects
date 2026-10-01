using GameEngine.Models;
using GameEngine.Tests.Constants;
using GameEngine.Tests.TestData;
using Xunit;

namespace GameEngine.Tests.Models
{
    [Trait(TestCategories.TraitName, TestCategories.Npc)]
    public class NonPlayerCharacterShould
    {
        [Theory]
        [HealthDamageData]
        public void TakeDamage(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
    }
}
