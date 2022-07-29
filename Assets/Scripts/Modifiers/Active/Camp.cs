using UnityEngine;

/// <summary>
/// A type of object that is placed on the start of the path and acts as
/// a base camp.
/// Upon entered by the player heals for a determined amount.
/// </summary>
[CreateAssetMenu(menuName = "ScriptablesObjects/Objects/Base Camp")]
public class Camp : TileModifier
{
    public int HealAmount = 100;

    public override void OnEntered()
    {
        BattleManager.Instance.Player.Heal(HealAmount);

        EventUIManager.Instance.CloseCardMenu += FinishRest;
        EventUIManager.Instance.OpenCardInterface();
    }

    private void FinishRest()
    {
        EventUIManager.Instance.CloseCardMenu -= FinishRest;

        EndEvent();
    }
}
