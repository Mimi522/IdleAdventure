using UnityEngine;

/* 
-- Class representing a tile in the grid containing the visual, for both
-- tile appearance and object on it, and the object's data.
*/
public class Tile
{
    // GameObject representing the tile
    private GameObject _tileVisual;
    public GameObject TileVisual {
        get { return _tileVisual; }
    }

    // GameObject over the tile representing the object on it
    private GameObject _objectVisual;

    // Object information and actions
    private Object _objectData;
    public Object ObjectData {
        get { return _objectData; }
        set { _objectData = value; }
    }

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

    // Placing an object in a tile only if the tile is empty
    public bool HoldObject(Object objectPrefab)
    {
        if (_objectData != null) {
            return false;
        }

        Vector3 objectPosition = _tileVisual.transform.position;

        _objectData = objectPrefab;
        _objectVisual = GameObject.Instantiate(objectPrefab.VisualPrefab, objectPosition, Quaternion.identity);

        return true;
    }
}
