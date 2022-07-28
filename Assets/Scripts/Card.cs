using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class representing a clickable card that contains a modifier.
/// </summary>
[RequireComponent(typeof(Button))]
public class Card : MonoBehaviour
{
    [SerializeField] private TileModifier _modifier;
    public TileModifier Modifier {
        get { return _modifier; }
    }

    public event Action<Card> Clicked;

    void OnValidate()
    {
        if (_modifier == null) {
            Debug.LogError("Missing card modifier.");
        }
    }

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
