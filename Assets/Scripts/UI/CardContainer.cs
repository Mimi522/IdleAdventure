using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that displays the possible cards to be used.
/// </summary>
[RequireComponent(typeof(CardDeck), typeof(TileSelector))]
public class CardContainer : MonoBehaviour
{
    public static CardContainer Instance { get; private set; }

    public event Action<Card> UpdateText;
    public event Action<int> UpdateActions;
    public event Action CloseMenu;

    [SerializeField] private GameObject _cardInventory;
    [SerializeField] private GameObject _cardInformation;
    [SerializeField] private GameObject _remainingActions;
    [SerializeField] private int _amountOfCards = 3;
    [SerializeField] private int _maxUses = 2;

    private Card _selectedCard;
    private CardDeck _cardDeck;
    private Card[] _cardPrefabs;
    private List<GameObject> _cardsOnHand = new List<GameObject>();
    private TileSelector _tileSelector;

    private int _usesRemaining;
    private bool _gameEnd = false;

    void OnValidate()
    {
        if (_amountOfCards <= 0) {
            Debug.LogError("Number of cards available can't be less than zero");
        }
    }

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
        _cardDeck = GetComponent<CardDeck>();
        _tileSelector = GetComponent<TileSelector>();
        RoundCounter.Instance.WinAchieved += () => { _gameEnd = true; };
    }

    void Update()
    {
        if (_selectedCard == null) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Tile tileSelected = _tileSelector.GetTileClicked(Input.mousePosition);

            if (tileSelected == null) {
                return;
            }

            bool placed = tileSelected.TryApplyModifier(_selectedCard.Modifier);

            if (placed == false) {
                return;
            }

            _cardInformation.SetActive(false);
            _cardsOnHand.Remove(_selectedCard.gameObject);
            Destroy(_selectedCard.gameObject);
            _selectedCard = null;
            DisplayCards(1);

            _usesRemaining--;

            if (_usesRemaining == 0) {
                _remainingActions.SetActive(false);
                HideUI();
                return;
            }

            UpdateActions?.Invoke(_usesRemaining);
        }
    }

    public void SelectCard(Card card)
    {
        _selectedCard = card;

        _cardInformation.SetActive(true);
        UpdateText?.Invoke(card);
    }

    public void ShowUI()
    {
        if (_gameEnd == true) {
            return;
        }

        RoundCounter.Instance.ShowUI(false);

        _cardInventory.SetActive(true);

        DisplayCards(_amountOfCards);

        _usesRemaining = _maxUses;
        _remainingActions.SetActive(true);

        UpdateActions?.Invoke(_usesRemaining);
    }

    private void HideUI()
    {
        foreach (GameObject card in _cardsOnHand) {
            card.GetComponent<Card>().Clicked -= SelectCard;
            Destroy(card);
        }

        _cardsOnHand.Clear();
        _cardInventory.SetActive(false);
        _remainingActions.SetActive(false);

        CloseMenu?.Invoke();

        RoundCounter.Instance.ShowUI(true);
    }

    private void DisplayCards(int amount)
    {
        _cardPrefabs = _cardDeck.DrawCards(amount);

        for (int i = 0; i < _cardPrefabs.Length; i++) {
            GameObject cardVisual = Instantiate(_cardPrefabs[i].gameObject, _cardInventory.transform);
            cardVisual.GetComponent<Image>().sprite = _cardPrefabs[i].Modifier.CardSprite;
            cardVisual.GetComponent<Card>().Clicked += SelectCard;
            _cardsOnHand.Add(cardVisual);
        }
    }
}
