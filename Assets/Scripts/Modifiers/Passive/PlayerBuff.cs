using UnityEngine;

/// <summary>
/// Scriptable object representing a passive modifier that generates a buff to the player.
/// </summary>
[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Player Buff")]
public class PlayerBuff : TileModifier
{
    public int Flat;
    public float Percentage;

    public override void OnPlaced()
    {
        BattleManager.Instance.Player.DamageDealtBonus.Percentage += Percentage;
        BattleManager.Instance.Player.DamageDealtBonus.Flat += Flat;
    }
}
