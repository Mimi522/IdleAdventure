using System;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    public static WaveCounter Instance { get; private set; }

    public event Action WinAchieved;

    [SerializeField] private GameObject _countDisplay;
    [SerializeField] private GameObject _textDisplay;

    private int _waveCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void IncrementWave()
    {
        _waveCount++;
        _textDisplay.GetComponent<TextMeshProUGUI>().text = $"Wave {_waveCount}/10";

        if (_waveCount == 10) {
            WinAchieved?.Invoke();
        }
    }
}
