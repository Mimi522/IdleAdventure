using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the game round counter UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class RoundUI : MonoBehaviour
{
    // Null reference error when trying to save it on variable after 
    // getcomponent call
    [SerializeField] private TextMeshProUGUI _textDisplay;

    void OnEnable()
    {
        RoundCounter.Instance.RoundCountChange += UpdateText;
    }

    void OnDisable()
    {
        RoundCounter.Instance.RoundCountChange -= UpdateText;
    }

    private void UpdateText(int amount)
    {
        _textDisplay.text = $"Round {amount}/10";
    }
}
