using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardDeck))]
public class InventoryUI : MonoBehaviour
{
    private CardDeck _cardDeck;
    private Card[] _cardPrefabs;
    public List<GameObject> _cardsOnHand { get; private set; } = new List<GameObject>();

    void Awake()
    {
        _cardDeck = GetComponent<CardDeck>();
    }

    public void DisplayCards(int amount)
    {
        _cardPrefabs = _cardDeck.DrawCards(amount);

        for (int i = 0; i < _cardPrefabs.Length; i++) {
            GameObject cardVisual = Instantiate(_cardPrefabs[i].gameObject, transform);
            cardVisual.GetComponent<Image>().sprite = _cardPrefabs[i].Modifier.CardSprite;
            _cardsOnHand.Add(cardVisual);
        }
    }
}
