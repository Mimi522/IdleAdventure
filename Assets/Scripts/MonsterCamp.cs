using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Monster Camp")]
public class MonsterCamp : Object
{
    public GameObject[] Monsters;

    private BattleManager _battleManager;

    public override void OnEntered()
    {
        _battleManager = BattleManager.Instance;
        _battleManager.OnBattleEnded += OnBattleEnded;
        _battleManager.StartBattle(Monsters);
    }

    private void OnBattleEnded()
    {
        _battleManager.OnBattleEnded -= OnBattleEnded;
        EndEvent();
    }
}
