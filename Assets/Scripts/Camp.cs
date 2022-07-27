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

    private BattleManager _battleManager;
    private CardContainer _cardContainer;

    public override void OnEntered()
    {
        _battleManager = BattleManager.Instance;
        _battleManager.Player.Heal(HealAmount);

        _cardContainer = CardContainer.Instance;
        _cardContainer.CardHand.SetActive(true);
        _cardContainer.CloseMenu += FinishRest;
    }

    private void FinishRest()
    {
        _cardContainer.CloseMenu -= FinishRest;
        _cardContainer.CardHand.SetActive(false);
        EndEvent();
    }
}
