using TMPro;
using UnityEngine;

/// <summary>
/// UI text displaying the information of the modifiers cards.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class CardInformation : MonoBehaviour
{
    private TextMeshProUGUI _textCamp;

    void Awake()
    {
        _textCamp = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCardInformation(TileModifier cardSelected)
    {
        string information = cardSelected.DisplayName + "\n" + cardSelected.Description;
        _textCamp.text = information;
    }
}
