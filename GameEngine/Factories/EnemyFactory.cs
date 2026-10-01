using System;
using System.Linq;
using GameEngine.Constants;
using GameEngine.Exceptions;
using GameEngine.Models;

namespace GameEngine.Factories
{
    public class EnemyFactory
    {
        public Enemy Create(string name, bool isBoss = false)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (isBoss)
            {
                if (!IsValidBossName(name))
                {
                    throw new EnemyCreationException(
                        $"{name} is not a valid name for a Boss enemy, Boss enemy names must end with " +
                        string.Join(" or ", GameConstants.BossNameSuffixes.Select(suffix => $"'{suffix}'")),
                        name);
                }

                return new BossEnemy { Name = name };
            }

            return new NormalEnemy { Name = name };
        }

        private static bool IsValidBossName(string name) =>
            GameConstants.BossNameSuffixes.Any(name.EndsWith);
    }
}
