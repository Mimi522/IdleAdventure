using System;
using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    public static RoundCounter Instance { get; private set; }

    public event Action WinAchieved;

    [SerializeField] private GameObject _countDisplay;
    [SerializeField] private GameObject _textDisplay;

    [SerializeField] private int _maxRounds = 10;

    private int _roundCount = 1;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        _textDisplay.GetComponent<TextMeshProUGUI>().text = $"Round {_roundCount}/10";
    }

    public void IncrementWave()
    {
        _roundCount++;
        _textDisplay.GetComponent<TextMeshProUGUI>().text = $"Round {_roundCount}/10";

        if (_roundCount == _maxRounds) {
            WinAchieved?.Invoke();
        }
    }

    public void ShowUI(bool state)
    {
        _countDisplay.SetActive(state);
    }
}
