namespace GameEngine.Models
{
    public abstract class Enemy
    {
        public string Name { get; set; } = "Aleku";
        public abstract double TotalSpecialPower { get; }
        public abstract double SpecialPowerUses { get; }
        public double SpecialAttackPower => TotalSpecialPower / SpecialPowerUses;
    }
}
