using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the damage pop up UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DamagePopUpUI : MonoBehaviour
{
    private TextMeshProUGUI _textDisplay;

    void Awake()
    {
        _textDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int value, float minSize, float maxSize)
    {
        _textDisplay.text = value.ToString();
        _textDisplay.fontSize = Random.Range(minSize, maxSize);
    }
}
