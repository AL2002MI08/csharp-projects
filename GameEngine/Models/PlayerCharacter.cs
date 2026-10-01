using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameEngine.Constants;

namespace GameEngine.Models
{
    public class PlayerCharacter : INotifyPropertyChanged
    {
        private int _health = GameConstants.DefaultHealth;

        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? Nickname { get; set; }
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnPropertyChanged();
            }
        }
        public bool IsNoob { get; set; }
        public List<string> Weapons { get; set; }

        public event EventHandler<EventArgs>? PlayerSlept;
        public event PropertyChangedEventHandler? PropertyChanged;

        public PlayerCharacter()
        {
            FirstName = GenerateRandomFirstName();
            IsNoob = true;
            Weapons = new List<string>(GameConstants.StartingWeapons);
        }

        public void Sleep()
        {
            Health += CalculateHealthIncrease();

            OnPlayerSlept(EventArgs.Empty);
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(GameConstants.MinimumHealth, Health - damage);
        }

        protected virtual void OnPlayerSlept(EventArgs e)
        {
            PlayerSlept?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static int CalculateHealthIncrease() =>
            Random.Shared.Next(GameConstants.MinSleepHealthIncrease, GameConstants.MaxSleepHealthIncrease + 1);

        private static string GenerateRandomFirstName() =>
            GameConstants.RandomFirstNames[Random.Shared.Next(GameConstants.RandomFirstNames.Count)];
    }
}
