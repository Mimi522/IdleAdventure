using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject[] GrassPrefabsVariant;
    public GameObject PathPrefab;
    public GameObject PawnPrefab;
    public Object SpawnTile;

    private GameObject _playerPawn;

    [SerializeField]
    private int _rows = 9;
    [SerializeField]
    private int _columns = 6;

    [SerializeField]
    private float _spacing = 1f;

    [SerializeField]
    private List<Vector2> _pathPositions;

    [SerializeField]
    private float _playerSpeed = 5f;

    private Vector3 _newPosition;

    private Vector2 _startPosition;

    private int _pathIndex = 0;

    private bool _walking = true;

    private TileGrid _boardGrid;

    private Tile TargetTile {
        get { return _boardGrid.GetTile((int)_pathPositions[_pathIndex].x, (int)_pathPositions[_pathIndex].y); }
    }

    void Awake()
    {
        _boardGrid = new TileGrid(_rows, _columns);
        SetInitialGrid();
        InstantiatePath();
        _startPosition = _pathPositions[0];
        InstantiatePlayer();
        SetTargetForPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Test()
    {
        _walking = true;
        TargetTile.ObjectData.OnEventEnded -= Test;
        SetTargetForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_walking == false)
            return;

        _playerPawn.transform.position = Vector3.MoveTowards(_playerPawn.transform.position, _newPosition, _playerSpeed * Time.deltaTime);
        _playerPawn.transform.LookAt(_newPosition);
        if ((_newPosition - _playerPawn.transform.position).magnitude <= 0.02f) {
            if (TargetTile.ObjectData != null) {
                CallEvent();
                return;
            }

            SetTargetForPlayer();
        }
    }

    private void SetInitialGrid()
    {
        for (int i = 0; i < _rows; i++) {
            for (int j = 0; j < _columns; j++) {
                int grassVariant = (i + j) % 2;

                Vector3 tilePosition = new Vector3(_spacing * j, 0, _spacing * i);

                Tile tile = new Tile(GrassPrefabsVariant[grassVariant], tilePosition);

                _boardGrid.SetTile(i, j, tile);
            }
        }
    }

    private void InstantiatePath()
    {
        for (int i = 0; i < _pathPositions.Count; i++) {
            // Invert those on inspector eventually
            Tile tile = _boardGrid.GetTile((int)_pathPositions[i].x, (int)_pathPositions[i].y);
            tile.UpdateTile(PathPrefab);

            if (i == 2 || i == 7)
                tile.ObjectData = SpawnTile;
        }
    }

    private void InstantiatePlayer()
    {
        Tile tile = _boardGrid.GetTile((int)_startPosition.x, (int)_startPosition.y);
        Vector3 tilePosition = tile.TileVisual.transform.position;

        Vector3 playerPosition = tilePosition;

        _playerPawn = Instantiate(PawnPrefab, playerPosition, Quaternion.identity);
    }

    private void SetTargetForPlayer()
    {
        _pathIndex++;

        if (_pathIndex == _pathPositions.Count)
            _pathIndex = 0;

        Vector3 tilePosition = TargetTile.TileVisual.transform.position;

        _newPosition = tilePosition;
    }

    private void CallEvent()
    {
        _walking = false;
        Tile tile = TargetTile;

        tile.ObjectData.OnEventEnded += Test;
        tile.ObjectData.OnEntered(_playerPawn);
    }
}
