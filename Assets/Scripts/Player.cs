using UnityEngine;

/// <summary>
/// Class representing the player entity
/// </summary>
public class Player : Entity
{
    public StatBonus HpBonus { get; set; } = new StatBonus();
    public StatBonus DamageDealtBonus { get; set; } = new StatBonus();
    public StatBonus AttackCooldownBonus { get; set; } = new StatBonus();

    protected override int Hp => ComputeStat(_baseStats.MaxHp, HpBonus);
    protected override int Damage => ComputeStat(_baseStats.DamageDealt, DamageDealtBonus);
    protected override int Cooldown => ComputeStat(_baseStats.AttackCooldown, AttackCooldownBonus);

    private int ComputeStat(int baseStat, StatBonus bonus)
    {
        float newValue = baseStat + baseStat * bonus.Percentage;
        newValue += bonus.Flat;

        return Mathf.FloorToInt(newValue);
    }
}
