using TMPro;
using UnityEngine;

/// <summary>
/// UI text displaying the information of the modifiers cards.
/// </summary>
public class CardInformation : MonoBehaviour
{
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
    }

    void OnEnable()
    {
        CardContainer.Instance.UpdateText += UpdateCardInformation;
    }

    void OnDisable()
    {
        CardContainer.Instance.UpdateText -= UpdateCardInformation;
    }

    public void UpdateCardInformation(Card cardSelected)
    {
        string information = cardSelected.Modifier.DisplayName + "\n" + cardSelected.Modifier.Description;
        _textCamp.text = information;
    }
}
