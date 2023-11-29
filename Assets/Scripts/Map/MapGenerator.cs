using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
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

        /// <summary>
        /// Creates a map of cells with a size register in map config
        /// </summary>
        private void InitializeMapGrid()
        {
            _mapGrid = new Cell[mapConfig.mapSize, mapConfig.mapSize];
            for (int x = 0; x < mapConfig.mapSize; x++)
            {
                for (int z = 0; z < mapConfig.mapSize; z++)
                {
                    _mapGrid[x, z] = Instantiate(cellPrefab, new Vector3(x * mapConfig.sizeScaler, 0, z * mapConfig.sizeScaler), Quaternion.identity, transform);
                    _mapGrid[x, z].transform.localScale *= mapConfig.sizeScaler;
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
            int x = (int)currentCell.transform.position.x;
            int z = (int)currentCell.transform.position.z;

            if (x + 1 < mapConfig.mapSize)
            {
                Cell cellToRight = _mapGrid[x + 1, z];
                if (cellToRight.isVisited == false) yield return cellToRight;
            }

            if (x - 1 >= 0)
            {
                Cell cellToLeft = _mapGrid[x - 1, z];
                if (cellToLeft.isVisited == false) yield return cellToLeft;
            }

            if (z + 1 < mapConfig.mapSize)
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
        /// Clears walls that obstruct other cells
        /// </summary>
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
    
        /// <summary>
        /// Generates a circuit as a list of cell of a size based on the map config
        /// Clears walls on map to display the race circuit and hides unused cells 
        /// </summary>
        private void GenerateCircuit()
        {
            // get circuit based on size
            int circuitSize = mapConfig.circuitSize > _cells.Count ? _cells.Count : mapConfig.circuitSize;
            _circuit = _cells.Take(circuitSize).ToList();

            // clear walls of selected cells
            foreach (Cell cell in _circuit)
            {
                ClearWalls(cell.previous, cell);
            }

            // hide all cells that aren't in the circuit
            foreach (Cell cell in _cells)
            {
                if (_circuit.All(circuitCell => circuitCell != cell)) cell.Hide();
            }
        }
    }
}