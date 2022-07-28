using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the remaining actions UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class RemainingActionsUI : MonoBehaviour
{
    private TextMeshProUGUI _textDisplay;

    void Awake()
    {
        _textDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int amount)
    {
        _textDisplay.text = $"Please use {amount} card{(amount > 1 ? "s" : "")}";
    }
}
