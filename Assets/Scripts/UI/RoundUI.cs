using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RoundUI : MonoBehaviour
{
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
        GetComponent<TextMeshProUGUI>().text = $"Round {amount}/10";
    }
}
