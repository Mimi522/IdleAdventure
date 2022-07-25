using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private TileBase _tileData;
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

    public Tile(TileBase tileData, Vector3 visualPosition)
    {
        _tileData = tileData;
        _tileVisual = GameObject.Instantiate(_tileData.TilePrefab, visualPosition, Quaternion.identity);
    }

    public void UpdateTile(TileBase tileData)
    {
        _tileData = tileData;
        Vector3 tilePosition = _tileVisual.transform.position;

        GameObject.Destroy(_tileVisual);
        _tileVisual = GameObject.Instantiate(_tileData.TilePrefab, tilePosition, Quaternion.identity);
    }
}
