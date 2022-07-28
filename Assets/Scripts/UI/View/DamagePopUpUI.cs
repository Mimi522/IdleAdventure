using TMPro;
using UnityEngine;

/// <summary>
/// Class responsible for updating the damage pop up UI. 
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DamagePopUpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;

    public void UpdateText(int value, float minSize, float maxSize)
    {
        _textDisplay.text = value.ToString();
        _textDisplay.fontSize = Random.Range(minSize, maxSize);
    }
}
