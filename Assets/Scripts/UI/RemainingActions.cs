using TMPro;
using UnityEngine;

/// <summary>
/// UI text displaying the amount of actions remaining.
/// </summary>
public class RemainingActions : MonoBehaviour
{
    private CardContainer _cardContainer;

    private TextMeshProUGUI _textCamp;

    void OnValidate()
    {
        _textCamp = GetComponentInChildren<TextMeshProUGUI>();

        if (_textCamp == null) {
            Debug.LogError("Missing text component in children.");
        }
    }

    void Awake()
    {
        _textCamp = GetComponentInChildren<TextMeshProUGUI>();
        _cardContainer = CardContainer.Instance;
    }

    void OnEnable()
    {
        _cardContainer.UpdateActions += UpdateRemainingActions;
    }

    void OnDisable()
    {
        _cardContainer.UpdateActions -= UpdateRemainingActions;
    }

    public void UpdateRemainingActions(int amount)
    {
        _textCamp.text = $"Please use {amount} card{(amount > 1 ? "s" : "")}";
    }
}
