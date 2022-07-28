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
    private WaveCounter _waveCounter;

    public override void OnEntered()
    {
        _battleManager = BattleManager.Instance;
        _battleManager.Player.Heal(HealAmount);

        _cardContainer = CardContainer.Instance;
        _cardContainer.CloseMenu += FinishRest;
        _cardContainer.ShowUI(); ;
    }

    private void FinishRest()
    {
        _cardContainer.HideUI();
        _cardContainer.CloseMenu -= FinishRest;

        _waveCounter = WaveCounter.Instance;
        _waveCounter.IncrementWave();

        EndEvent();
    }
}
