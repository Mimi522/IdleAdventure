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

    void OnEnable()
    {
        CardContainer.Instance.UpdateActions += UpdateText;
    }

    void OnDisable()
    {
        CardContainer.Instance.UpdateActions -= UpdateText;
    }

    private void UpdateText(int amount)
    {
        _textDisplay.text = $"Please use {amount} card{(amount > 1 ? "s" : "")}";
    }
}
