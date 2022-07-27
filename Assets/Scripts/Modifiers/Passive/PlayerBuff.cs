using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Player Buff")]
public class PlayerBuff : TileModifier
{
    public int Flat;
    public float Percentage;

    private BattleManager _battleManager;

    public override void OnPlaced()
    {
        _battleManager = BattleManager.Instance;
        _battleManager.Player.DamageDealtBonus.Percentage += Percentage;
        _battleManager.Player.DamageDealtBonus.Flat += Flat;
    }
}
