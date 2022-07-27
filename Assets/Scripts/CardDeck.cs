using UnityEngine;

/// <summary>
/// Class container of avaiable cards
/// </summary>
public class CardDeck : MonoBehaviour
{
    [SerializeField] private Card[] Cards;

    void OnValidate()
    {
        if (Cards.Length == 0) {
            Debug.LogError("Deck has no cards.");
        }
    }

    public Card[] DrawCards(int amount)
    {
        if (amount <= 0) {
            return null;
        }

        Card[] cardsDrawn = new Card[amount];

        for (int i = 0; i < cardsDrawn.Length; i++) {
            cardsDrawn[i] = Cards[Random.Range(0, Cards.Length)];
        }

        return cardsDrawn;
    }
}
