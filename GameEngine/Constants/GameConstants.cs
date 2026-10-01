using System.Collections.Generic;

namespace GameEngine.Constants
{
    public static class GameConstants
    {
        public const int DefaultHealth = 100;
        public const int MinimumHealth = 1;
        public const int MinSleepHealthIncrease = 1;
        public const int MaxSleepHealthIncrease = 100;
        public const int EarthquakeDamage = 25;

        public static readonly IReadOnlyList<string> StartingWeapons = new[]
        {
            "Long Bow",
            "Short Bow",
            "Short Sword"
        };

        public static readonly IReadOnlyList<string> RandomFirstNames = new[]
        {
            "Danieth",
            "Derick",
            "Shalnorr",
            "G'Toth'lop",
            "Boldrakteethtop"
        };

        public static readonly IReadOnlyList<string> BossNameSuffixes = new[]
        {
            "King",
            "Queen"
        };
    }
}
