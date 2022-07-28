using TMPro;
using UnityEngine;

/// <summary>
/// UI text displaying the amount of actions remaining.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class RemainingActionsUI : MonoBehaviour
{
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
        GetComponentInChildren<TextMeshProUGUI>().text = $"Please use {amount} card{(amount > 1 ? "s" : "")}";
    }
}
