using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class containing a modifier that can be applied in the board game.
/// </summary>
[RequireComponent(typeof(Button))]
public class Card : MonoBehaviour
{
    [SerializeField] private TileModifier _modifier;
    public TileModifier Modifier {
        get { return _modifier; }
    }

    public event Action<Card> Clicked;

    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke(this);
    }

    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}