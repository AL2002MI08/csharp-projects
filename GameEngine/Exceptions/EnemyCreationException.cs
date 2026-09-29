using System;

namespace GameEngine.Exceptions
{
    public class EnemyCreationException(string message, string enemyName) : Exception(message)
    {
        public string RequestedEnemyName { get; } = enemyName;
    }
}
