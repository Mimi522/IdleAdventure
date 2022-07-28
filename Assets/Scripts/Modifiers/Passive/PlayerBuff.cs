using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
