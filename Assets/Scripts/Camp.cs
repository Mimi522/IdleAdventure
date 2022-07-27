using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
