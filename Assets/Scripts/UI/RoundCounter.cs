using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Count the rounds passed and enable/disable ui.
/// </summary>
public class RoundCounter : MonoBehaviour
{
    public static RoundCounter Instance { get; private set; }

    [SerializeField] private int _maxRounds = 10;

    public event Action WinAchieved;
    public UnityEvent<int> OnFinishRound;

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
        OnFinishRound?.Invoke(_roundCount);
    }

    public void IncrementWave()
    {
        _roundCount++;

        if (_roundCount >= _maxRounds) {
            WinAchieved?.Invoke();
        }

        OnFinishRound?.Invoke(_roundCount);
    }
}
