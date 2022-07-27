/// <summary>
/// Struct with damage information inflicted by an entity.
/// </summary>
public struct DamageInfo
{
    public int Damage;

    public static DamageInfo Single(int damage)
    {
        return new DamageInfo { Damage = damage };
    }
}
