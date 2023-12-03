using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private MapConfig _mapConfig;
    private Cell[,] _mapGrid;
    private List<Cell> _circuit = new List<Cell>();
    private readonly List<Cell> _cells = new List<Cell>();

    public Map GetMap(MapConfig mapConfig)
    {
        if (mapConfig == null)
        {
            Debug.LogError("MAP GENERATOR: map config is null");
            return null;
        }

        _mapConfig = mapConfig;
        
        InitializeMapGrid();
        GenerateMap(null, _mapGrid[0, 0]);
        SelectCell();

        return new Map(_circuit);
    }

    /// <summary>
    /// Creates a map of cells with a size register in map config
    /// </summary>
    private void InitializeMapGrid()
    {
        _mapGrid = new Cell[_mapConfig.mapSize, _mapConfig.mapSize];
        for (int x = 0; x < _mapConfig.mapSize; x++)
        {
            for (int z = 0; z < _mapConfig.mapSize; z++)
            {
                // instantiate circuit
                _mapGrid[x, z] = Instantiate(_mapConfig.cellPrefab, new Vector3(x * _mapConfig.sizeScaler, 0, z * _mapConfig.sizeScaler), Quaternion.identity, transform);
                _mapGrid[x, z].pos = new Vector2(x, z);
                _mapGrid[x, z].transform.localScale *= _mapConfig.sizeScaler;
            }
        }
    }

    /// <summary>
    /// Adds a cell to the cells list. Calls itself to add the next cell.
    /// </summary>
    private void GenerateMap(Cell previousCell, Cell currentCell)
    {
        // update display of the current cell
        currentCell.Visit();
        currentCell.previous = previousCell;
        _cells.Add(currentCell);
    
        // generate next cells
        Cell nextCell;
        do {
            nextCell = GetNextUnvisitedCell(currentCell);
            currentCell.next = nextCell;
            if (nextCell != null) GenerateMap(currentCell, nextCell);
        } while (nextCell != null);
    }

    /// <summary>
    /// Returns a randomly selected cell from the nearby cells
    /// </summary>
    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    /// <summary>
    /// Returns a ienumerable list of cells that contains every nearby unvisited cells
    /// </summary>
    private IEnumerable<Cell> GetUnvisitedCells(Cell currentCell)
    {
        int x = (int)currentCell.transform.position.x / _mapConfig.sizeScaler;
        int z = (int)currentCell.transform.position.z / _mapConfig.sizeScaler;

        if (x + 1 < _mapConfig.mapSize)
        {
            Cell cellToRight = _mapGrid[x + 1, z];
            if (cellToRight.isVisited == false) yield return cellToRight;
        }

        if (x - 1 >= 0)
        {
            Cell cellToLeft = _mapGrid[x - 1, z];
            if (cellToLeft.isVisited == false) yield return cellToLeft;
        }

        if (z + 1 < _mapConfig.mapSize)
        {
            Cell cellToFront = _mapGrid[x, z + 1];
            if (cellToFront.isVisited == false) yield return cellToFront;
        }

        if (z - 1 >= 0)
        {
            Cell cellToBack = _mapGrid[x, z - 1];
            if (cellToBack.isVisited == false) yield return cellToBack;
        }
    }

    /// <summary>
    /// Generates a circuit as a list of cell of a size based on the map config
    /// Clears walls on map to display the race circuit and hides unused cells 
    /// </summary>
    private void SelectCell()
    {
        // get circuit based on size
        int circuitSize = _mapConfig.circuitSize > _cells.Count ? _cells.Count : _mapConfig.circuitSize;
        _circuit = _cells.Take(circuitSize).ToList();

        // hide all unused cells
        foreach (Cell cell in _cells)
        {
            if (_circuit.All(circuitCell => circuitCell != cell)) cell.Hide();
        }
    }
}