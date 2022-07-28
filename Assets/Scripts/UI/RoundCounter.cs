using System;
using UnityEngine;

/// <summary>
/// Count the rounds passed and enable/disable ui.
/// </summary>
public class RoundCounter : MonoBehaviour
{
    public static RoundCounter Instance { get; private set; }

    public event Action WinAchieved;
    public event Action<int> RoundCountChange;

    [SerializeField] private GameObject _countDisplay;

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
        RoundCountChange?.Invoke(_roundCount);
    }

    public void IncrementWave()
    {
        _roundCount++;

        if (_roundCount >= _maxRounds) {
            WinAchieved?.Invoke();
        }

        RoundCountChange?.Invoke(_roundCount);
    }

    public void ShowUI(bool state)
    {
        _countDisplay.SetActive(state);
    }
}
