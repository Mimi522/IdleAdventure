using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class that displays the possible cards to be used.
/// </summary>
[RequireComponent(typeof(TileSelector))]
public class CardContainer : MonoBehaviour
{
    [SerializeField] private int _amountOfCards = 3;
    [SerializeField] private int _maxUses = 2;

    [SerializeField] private InventoryUI _cardInventory;

    public UnityEvent<int> OnTakingAction;
    public UnityEvent<TileModifier> OnSelectingCard;


    private Card _selectedCard;
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

            UseCard();

            if (_usesRemaining == 0) {
                DiscardCards();
                return;
            }

            _cardInventory.DisplayCards(1);
            SetCardsCallbacks();
            OnTakingAction?.Invoke(_usesRemaining);
        }
    }

    public void SetCards()
    {
        _cardInventory.DisplayCards(_amountOfCards);

        SetCardsCallbacks();

        _usesRemaining = _maxUses;
        OnTakingAction?.Invoke(_usesRemaining);
    }

    public void SelectCard(Card card)
    {
        _selectedCard = card;

        EventUIManager.Instance.ShowCardInformation(true);
        OnSelectingCard?.Invoke(card.Modifier);
    }

    private void SetCardsCallbacks()
    {
        foreach (GameObject card in _cardInventory.CardsOnHand) {
            card.GetComponent<Card>().Clicked += SelectCard;
        }
    }

    private void DiscardCards()
    {
        foreach (GameObject card in _cardInventory.CardsOnHand) {
            card.GetComponent<Card>().Clicked -= SelectCard;
            Destroy(card);
        }

        _cardInventory.CardsOnHand.Clear();

        EventUIManager.Instance.CloseCardInterface();
    }

    private void UseCard()
    {
        EventUIManager.Instance.ShowCardInformation(false);

        _cardInventory.CardsOnHand.Remove(_selectedCard.gameObject);
        Destroy(_selectedCard.gameObject);

        _selectedCard = null;
        _usesRemaining--;
    }
}
