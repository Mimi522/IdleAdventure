using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private Object _objectData;
    public Object ObjectData {
        get { return _objectData; }
        set { _objectData = value; }
    }

    private GameObject _tileVisual;
    public GameObject TileVisual {
        get { return _tileVisual; }
    }

    private GameObject _objectVisual;

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

    public bool HoldObject(Object objectPrefab)
    {
        if (_objectData != null)
            return false;

        Vector3 objectPosition = _tileVisual.transform.position;

        _objectData = objectPrefab;
        _objectVisual = GameObject.Instantiate(objectPrefab.VisualPrefab, objectPosition, Quaternion.identity);

        return true;
    }
}
