using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Spawner")]
public class SpawnerObject : Object
{
    public BattleManager battleManager;

    public GameObject[] enemy;

    public override void OnEntered(GameObject Player)
    {
        battleManager = BattleManager.Instance;
        battleManager.OnBattleEnded += OnBattleEnded;
        battleManager.StartBattle(Player, enemy);
    }

    private void OnBattleEnded()
    {
        battleManager.OnBattleEnded -= OnBattleEnded;
        EndEvent();
    }
}
