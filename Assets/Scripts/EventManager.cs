using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action CloseCardMenu;

    [SerializeField] private GameObject _roundsDisplay;
    [SerializeField] private GameObject _inventoryDisplay;
    [SerializeField] private GameObject _actionsDisplay;

    [SerializeField] private RoundCounter _roundCounter;
    [SerializeField] private CardContainer _cardContainer;

    private bool _gameEnd = false;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void OnEnable()
    {
        _roundCounter.WinAchieved += () => { _gameEnd = true; };
    }

    public void OpenCardInterface()
    {
        _roundCounter.IncrementWave();

        if (_gameEnd == true) {
            return;
        }

        _roundsDisplay.SetActive(false);

        _inventoryDisplay.SetActive(true);
        _actionsDisplay.SetActive(true);

        _cardContainer.SetCards();
    }

    public void CloseCardInterface()
    {
        _inventoryDisplay.SetActive(false);
        _actionsDisplay.SetActive(false);

        CloseCardMenu?.Invoke();

        _roundsDisplay.SetActive(true);
    }
}
