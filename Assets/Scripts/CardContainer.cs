using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that displays the possible cards to be used.
/// </summary>
[RequireComponent(typeof(CardDeck), typeof(TileSelector))]
public class CardContainer : MonoBehaviour
{
    public static CardContainer Instance { get; private set; }

    public event Action CloseMenu;

    [SerializeField] private GameObject _cardInventory;
    [SerializeField] private int _amountOfCards = 3;
    [SerializeField] private int _maxUses = 2;

    private Card _selectedCard;
    private CardDeck _cardDeck;
    private Card[] _cardPrefabs;
    private List<GameObject> _cardsOnHand;
    private TileSelector _tileSelector;

    private int _usesRemaining;

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

        _cardDeck = GetComponent<CardDeck>();
        _tileSelector = GetComponent<TileSelector>();
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

            _cardsOnHand.Remove(_selectedCard.gameObject);
            Destroy(_selectedCard.gameObject);
            _selectedCard = null;
            DisplayCards(1);

            _usesRemaining--;

            if (_usesRemaining == 0) {
                CloseMenu?.Invoke();
                return;
            }
        }
    }

    public void SelectCard(Card card)
    {
        _selectedCard = card;
    }

    public void ShowUI()
    {
        _cardInventory.SetActive(true);

        _cardsOnHand = new List<GameObject>();

        DisplayCards(_amountOfCards);

        _usesRemaining = _maxUses;
    }

    public void HideUI()
    {
        foreach (GameObject card in _cardsOnHand) {
            card.GetComponent<Card>().Clicked -= SelectCard;
            Destroy(card);
        }

        _cardsOnHand.Clear();
        _cardInventory.SetActive(false);
    }

    private void DisplayCards(int amount)
    {
        _cardPrefabs = _cardDeck.DrawCards(amount);

        for (int i = 0; i < _cardPrefabs.Length; i++) {
            GameObject cardVisual = Instantiate(_cardPrefabs[i].gameObject, _cardInventory.transform);
            cardVisual.GetComponent<Card>().Clicked += SelectCard;
            _cardsOnHand.Add(cardVisual);
        }
    }
}
