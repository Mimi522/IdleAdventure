using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the remaining actions UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class RemainingActionsUI : MonoBehaviour
{
    // Null reference error when trying to save it on variable after 
    // getcomponent call
    [SerializeField] private TextMeshProUGUI _textDisplay;

    public void UpdateText(int amount)
    {
        _textDisplay.text = $"Please use {amount} card{(amount > 1 ? "s" : "")}";
    }
}
