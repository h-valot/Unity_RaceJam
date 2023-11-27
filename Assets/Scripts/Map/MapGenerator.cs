using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Cell cellPrefab;
    public MapConfig mapConfig;
    
    private Cell[,] _mapGrid;
    private List<Cell> _cells = new List<Cell>();
    private List<Cell> _circuit = new List<Cell>();

    void Start()
    {
        InitializeMapGrid();
        GenerateMap(null, _mapGrid[0, 0]);
        GenerateCircuit();
    }

    private void InitializeMapGrid()
    {
        _mapGrid = new Cell[mapConfig.mapSize, mapConfig.mapSize];
        for (int x = 0; x < mapConfig.mapSize; x++)
        {
            for (int z = 0; z < mapConfig.mapSize; z++)
            {
                _mapGrid[x, z] = Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
            }
        }
    }

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

    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<Cell> GetUnvisitedCells(Cell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < mapConfig.mapSize)
        {
            var cellToRight = _mapGrid[x + 1, z];
            if (cellToRight.isVisited == false) yield return cellToRight;
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mapGrid[x - 1, z];
            if (cellToLeft.isVisited == false) yield return cellToLeft;
        }

        if (z + 1 < mapConfig.mapSize)
        {
            var cellToFront = _mapGrid[x, z + 1];
            if (cellToFront.isVisited == false) yield return cellToFront;
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mapGrid[x, z - 1];
            if (cellToBack.isVisited == false) yield return cellToBack;
        }
    }

    private void ClearWalls(Cell previousCell, Cell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
    
    private void GenerateCircuit()
    {
        // get circuit based on size
        int circuitSize = mapConfig.circuitSize > _cells.Count ? _cells.Count : mapConfig.circuitSize;
        _circuit = _cells.Take(circuitSize).ToList();

        // clear walls of selected cells
        foreach (Cell cell in _circuit)
        {
            cell.isInstantiate = true;
            ClearWalls(cell.previous, cell);
        }

        // hide all cells that aren't in the circuit
        foreach (Cell cell in _cells)
        {
            if (_circuit.All(circuitCell => circuitCell != cell)) cell.Hide();
        }
    }
}