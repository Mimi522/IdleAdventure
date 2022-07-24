using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public DefaultTileFactory[] GrassPrefabsVariant;
    public DefaultTileFactory NewLook;
    public GameObject playerPrefab;

    private GameObject player;

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

    private Vector3 newPosition = new Vector3(0, 1, 1);

    private Vector2 _startPosition = new Vector2(2, 1);

    private int _pathIndex = 0;

    private TileGrid boardGrid;

    void Awake()
    {
        boardGrid = new TileGrid(_rows, _columns);
        SetInitialGrid();
        InstantiatePath();
        SetStartPosition();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, newPosition, _playerSpeed * Time.deltaTime);
        player.transform.LookAt(newPosition);
        if ((newPosition - player.transform.position).magnitude <= 0.02f)
            SetTargetForPlayer();
    }

    private void SetInitialGrid()
    {
        for (int i = 0; i < _rows; i++) {
            for (int j = 0; j < _columns; j++) {
                int grassVariant = (i + j) % 2;

                Vector3 tilePosition = new Vector3(_spacing * j, 0, _spacing * i);

                Tile tile = new Tile(GrassPrefabsVariant[grassVariant], tilePosition);

                boardGrid.SetTile(i, j, tile);
            }
        }
    }

    private void InstantiatePath()
    {
        for (int i = 0; i < _pathPositions.Count; i++) {
            Tile tile = boardGrid.GetTile((int)_pathPositions[i].x - 1, (int)_pathPositions[i].y - 1);
            tile.UpdateTile(NewLook);
        }
    }

    private void SetStartPosition()
    {
        Tile tile = boardGrid.GetTile((int)_startPosition.x - 1, (int)_startPosition.y - 1);
        Vector3 tilePosition = tile.TileVisual.transform.position;

        Vector3 playerPosition = tilePosition + new Vector3(0, 1, 0);

        player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
    }

    public void SetTargetForPlayer()
    {
        Tile tile = boardGrid.GetTile((int)_pathPositions[_pathIndex].x - 1, (int)_pathPositions[_pathIndex].y - 1);

        Vector3 tilePosition = tile.TileVisual.transform.position;

        newPosition = tilePosition + new Vector3(0, 1, 0);

        _pathIndex++;

        if (_pathIndex == _pathPositions.Count)
            _pathIndex = 0;
    }
}
