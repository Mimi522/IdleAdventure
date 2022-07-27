using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type of object that is placed on the start of the path and acts as
/// a base camp.
/// Upon entered by the player heals for a determined amount.
/// </summary>
[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Base Camp")]
public class Camp : Object
{
    public int HealAmount = 100;

    private BattleManager _battleManager;

    public override void OnEntered()
    {
        _battleManager = BattleManager.Instance;
        _battleManager.Player.Heal(HealAmount);
        EndEvent();
    }
}
