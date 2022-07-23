using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public EmptyTile EmptyTile;

    [SerializeField]
    private int _rows = 9;
    [SerializeField]
    private int _columns = 6;

    [SerializeField]
    private float _spacing = 1.5f;

    private Grid boardGrid;

    void Awake()
    {
        boardGrid = new Grid(_rows, _columns);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInitialGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetInitialGrid()
    {
        for (int i = 0; i < _rows; i++) {
            for (int j = 0; j < _columns; j++) {
                Vector3 tilePosition = new Vector3(_spacing * j, 0, _spacing * i);

                GameObject tile = Instantiate(EmptyTile.TilePrefab, tilePosition, Quaternion.identity);
                boardGrid.SetTile(i, j, EmptyTile);
            }
        }
    }
}
