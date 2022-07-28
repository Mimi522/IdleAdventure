using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the game round counter UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class RoundUI : MonoBehaviour
{
    private TextMeshProUGUI _textDisplay;

    void Awake()
    {
        _textDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int amount)
    {
        _textDisplay.text = $"Round {amount}/10";
    }
}
