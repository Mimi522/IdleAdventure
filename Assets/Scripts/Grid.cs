using System;

public class Grid
{
    private int _rows;
    private int _columns;
    private TileTypeBase[,] _grid;

    public Grid(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentOutOfRangeException();

        _rows = rows;
        _columns = columns;

        _grid = new TileTypeBase[_rows, _columns];
    }

    public TileTypeBase GetTile(int row, int column)
    {
        CheckInvalidIndexAndThrow(row, column);

        return _grid[row, column];
    }

    public void SetTile(int row, int column, TileTypeBase tile)
    {
        CheckInvalidIndexAndThrow(row, column);

        _grid[row, column] = tile;
    }

    private void CheckInvalidIndexAndThrow(int row, int column)
    {
        bool isRowOutOfBounds = row < 0 || row > _rows;
        bool isColumnOutOfBounds = column < 0 || column > _columns;

        if (isRowOutOfBounds || isColumnOutOfBounds)
            throw new IndexOutOfRangeException();
    }
}