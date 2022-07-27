using UnityEngine;

/// <summary>
/// Class representing a tile in the grid containing the visual, for both
/// tile appearance and object on it, and the object's data.
/// </summary>
public class Tile : MonoBehaviour
{
    // GameObject representing the tile
    /*private GameObject _tileVisual;
    public GameObject TileVisual {
        get { return _tileVisual; }
    }*/

    // GameObject over the tile representing the object on it
    private GameObject _objectVisual;

    // Object information and actions
    private TileModifier _modifierData;
    public TileModifier ModifierData {
        get { return _modifierData; }
        set { _modifierData = value; }
    }

    /*
    public Tile(GameObject tilePrefab, Vector3 visualPosition)
    {
        _tileVisual = GameObject.Instantiate(tilePrefab, visualPosition, Quaternion.identity);
    }

    public void UpdateTile(GameObject tilePrefab)
    {
        Vector3 tilePosition = _tileVisual.transform.position;

        GameObject.Destroy(_tileVisual);
        _tileVisual = GameObject.Instantiate(tilePrefab, tilePosition, Quaternion.identity);
    }
    */
    // Placing an object in a tile only if the tile is empty
    public bool TryApplyModifier(TileModifier objectPrefab)
    {
        if (_modifierData != null) {
            return false;
        }

        Vector3 objectPosition = transform.position;

        _modifierData = objectPrefab;
        _objectVisual = GameObject.Instantiate(objectPrefab.VisualPrefab, objectPosition, Quaternion.identity);

        return true;
    }
}
