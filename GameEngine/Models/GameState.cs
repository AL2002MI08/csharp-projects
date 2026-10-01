using System;
using System.Collections.Generic;
using System.Threading;
using GameEngine.Constants;

namespace GameEngine.Models
{
    public class GameState
    {
        public List<PlayerCharacter> Players { get; set; } = new List<PlayerCharacter>();
        public Guid Id { get; } = Guid.NewGuid();

        public GameState()
        {
            CreateGameWorld();
        }

        public void Earthquake()
        {
            foreach (var player in Players)
            {
                player.TakeDamage(GameConstants.EarthquakeDamage);
            }
        }

        public void Reset()
        {
            Players.Clear();
        }

        private static void CreateGameWorld()
        {
            // Simulate expensive creation
            Thread.Sleep(2000);
        }
    }
}
