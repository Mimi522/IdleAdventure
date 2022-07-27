using UnityEngine;

/// <summary>
/// Class storing the visual and data of a modifier in the tile.
/// </summary>
public class Tile : MonoBehaviour
{
    private GameObject _modifierVisual;

    private TileModifier _modifierData;
    public TileModifier ModifierData {
        get { return _modifierData; }
        set { _modifierData = value; }
    }

    // Placing an object in a tile only if the tile is empty
    public bool TryApplyModifier(TileModifier objectPrefab)
    {
        if (_modifierData != null) {
            return false;
        }

        Vector3 objectPosition = transform.position;

        _modifierData = objectPrefab;
        _modifierVisual = GameObject.Instantiate(objectPrefab.VisualPrefab, objectPosition, Quaternion.identity);

        return true;
    }
}
