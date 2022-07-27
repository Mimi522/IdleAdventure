using System;
using UnityEngine;

/// <summary>
/// Class that displays the possible cards to be used.
/// </summary>
public class CardContainer : MonoBehaviour
{
    public static CardContainer Instance { get; private set; }

    public event Action CloseMenu;

    [SerializeField] private Card _cardTest;
    [SerializeField] private GameObject _cardHand;
    public GameObject CardHand {
        get { return _cardHand; }
    }

    private Card _selectedCard;
    private TileSelector _tileSelector;

    void OnEnable()
    {
        _cardTest.Clicked += SelectCard;
    }

    void OnDisable()
    {
        _cardTest.Clicked -= SelectCard;
    }

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        _tileSelector = GetComponent<TileSelector>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

            _selectedCard = null;
            CloseMenu?.Invoke();
        }
    }

    public void SelectCard(Card card)
    {
        _selectedCard = card;
    }
}
