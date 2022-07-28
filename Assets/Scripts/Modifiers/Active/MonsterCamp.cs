using UnityEngine;

/// <summary>
/// A type of object that is placed on the path and acts as a monster camp.
/// Upon entered by the player initializes a battle.
/// </summary>
[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Monster Camp")]
public class MonsterCamp : TileModifier
{
    public GameObject[] Monsters;

    public override void OnEntered()
    {
        if (Monsters.Length == 0) {
            return;
        }

        BattleManager.Instance.OnBattleEnded += OnBattleEnded;
        BattleManager.Instance.StartBattle(Monsters);
    }

    private void OnBattleEnded()
    {
        BattleManager.Instance.OnBattleEnded -= OnBattleEnded;
        EndEvent();
    }
}
