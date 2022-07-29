using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that creates the game board and manages the player pawn movement
/// throught it.
/// </summary>
public class GameBoard : MonoBehaviour
{
    public GameObject[] GrassPrefabsVariant;
    public GameObject PathPrefab;
    public GameObject PawnPrefab;

    public TileModifier BaseCamp;
    public TileModifier MonsterCamp;
    public TileModifier MonsterCamp2;
    public TileModifier BossCamp;

    private GameObject _playerPawn;

    [SerializeField] private int _rows = 9;
    [SerializeField] private int _columns = 6;

    [SerializeField] private float _spacing = 1f;
    [SerializeField] private float _playerSpeed = 5f;

    [SerializeField] private List<Vector2> _pathPositions;

    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _gameOverUI;

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
        RoundCounter.Instance.WinAchieved += WinGame;
        BattleManager.Instance.OnPlayerDeath += LoseGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (_walking == false)
            return;

        _playerPawn.transform.position = Vector3.MoveTowards(_playerPawn.transform.position, _newPosition, _playerSpeed * Time.deltaTime);
        _playerPawn.transform.LookAt(_newPosition);

        if ((_newPosition - _playerPawn.transform.position).magnitude <= 0.02f) {
            if (TargetTile.ModifierData != null) {
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

                GameObject tile = Instantiate(GrassPrefabsVariant[grassVariant], tilePosition, Quaternion.identity, transform);

                _boardGrid.SetTile(i, j, tile.GetComponent<Tile>());
            }
        }
    }

    private void InstantiatePath()
    {
        for (int i = 0; i < _pathPositions.Count; i++) {
            // Invert those on inspector eventually
            Tile tileData = _boardGrid.GetTile((int)_pathPositions[i].x, (int)_pathPositions[i].y);

            Vector3 tilePosition = tileData.transform.position;
            Destroy(tileData.gameObject);

            GameObject tile = Instantiate(PathPrefab, tilePosition, Quaternion.identity, transform);
            _boardGrid.SetTile((int)_pathPositions[i].x, (int)_pathPositions[i].y, tile.GetComponent<Tile>());

            tileData = tile.GetComponent<Tile>();
            tileData.TileType = TileType.Active;

            if (i == 0) {
                tileData.TryApplyModifier(BaseCamp);
            } else if (i == 4) {
                tileData.TryApplyModifier(MonsterCamp);
            } else if (i == 7) {
                tileData.TryApplyModifier(MonsterCamp2);
            } else if (i == 13) {
                tileData.TryApplyModifier(BossCamp);
            }
        }
    }

    private void InstantiatePlayer()
    {
        Tile tile = _boardGrid.GetTile((int)_startPosition.x, (int)_startPosition.y);
        Vector3 tilePosition = tile.transform.position;

        Vector3 playerPosition = tilePosition;

        _playerPawn = Instantiate(PawnPrefab, playerPosition, Quaternion.identity, transform);
    }

    private void SetTargetForPlayer()
    {
        _pathIndex++;

        if (_pathIndex == _pathPositions.Count)
            _pathIndex = 0;

        Vector3 tilePosition = TargetTile.transform.position;

        _newPosition = tilePosition;
    }

    private void CallEvent()
    {
        _walking = false;
        Tile tile = TargetTile;

        tile.ModifierData.OnEventEnded += Move;
        tile.ModifierData.OnEntered();
    }

    private void Move()
    {
        _walking = true;
        TargetTile.ModifierData.OnEventEnded -= Move;
        SetTargetForPlayer();
    }

    private void WinGame()
    {
        _walking = false;

        RoundCounter.Instance.WinAchieved -= WinGame;
        BattleManager.Instance.OnPlayerDeath -= LoseGame;

        _winUI.SetActive(true);
    }

    private void LoseGame()
    {
        _walking = false;

        RoundCounter.Instance.WinAchieved -= WinGame;
        BattleManager.Instance.OnPlayerDeath -= LoseGame;

        _gameOverUI.SetActive(true);
    }
}
