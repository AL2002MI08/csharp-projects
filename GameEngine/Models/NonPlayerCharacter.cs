using System;
using GameEngine.Constants;

namespace GameEngine.Models
{
    public class NonPlayerCharacter
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public int Health { get; set; } = GameConstants.DefaultHealth;

        public void TakeDamage(int damage)
        {
            Health = Math.Max(GameConstants.MinimumHealth, Health - damage);
        }
    }
}
