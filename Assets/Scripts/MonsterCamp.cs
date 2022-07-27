using UnityEngine;

/// <summary>
/// A type of object that is placed on the path and acts as a monster camp.
/// Upon entered by the player initializes a battle.
/// </summary>
[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Monster Camp")]
public class MonsterCamp : TileModifier
{
    public GameObject[] Monsters;

    private BattleManager _battleManager;

    public override void OnEntered()
    {
        if (Monsters.Length == 0) {
            return;
        }

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
