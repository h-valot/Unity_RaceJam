using System.Collections.Generic;
using System.Linq;
using List;
using UnityEngine;

public static class MapGenerator
{
    private static readonly List<List<Cell>> _cells = new List<List<Cell>>();
    private static List<Cell> _path = new List<Cell>();
    private static MapConfig _mapConfig;
    
    public static Map GetMap(MapConfig newMapConfig)
    {
        // exit if the new map config is empty
        if (newMapConfig == null)
        {
            Debug.LogError("MAP GENERATOR: map config is null");
            return null;
        }
        
        _mapConfig = newMapConfig;
        int iterationAmount = 0;
        
        GenerateCells();
        VisitCell(null, _cells[0][0]);
        
        Debug.Log($"MAP GENERATOR: path successfully generated with {iterationAmount} tries. there are {_path.Count} cells in the path.");
        
        // returns a list with cells that form the circuit
        return new Map(_path);
    }

    private static void GenerateCells()
    {
        for (int y = 0; y < _mapConfig.mapSize; y++)
        {
            _cells.Add(new List<Cell>()); 
            for (int x = 0; x < _mapConfig.mapSize; x++)
            {
                _cells[y].Add(new Cell(x, y));
            }
        }
    }

    private static void VisitCell(Cell previousCell, Cell currentCell)
    {
        var nextCell = GetNewUnvisitedCell(currentCell);

        if (nextCell != null)
        {
            VisitCell(currentCell, nextCell);
        }
    }

    private static Cell GetNewUnvisitedCell(Cell currentCell)
    {
        List<Cell> unvisitedCells = GetUnvisitedCells(currentCell);
        unvisitedCells.Shuffle();
        return unvisitedCells.FirstOrDefault();
    }
    
    private static List<Cell> GetUnvisitedCells(Cell currentCell)
    {
        List<Cell> output = new List<Cell>();
        
        int row = currentCell.y;
        int col = currentCell.x;
        
        if (row + 1 < _mapConfig.mapSize && _cells[col][row + 1].isVisited)
        {
            output.Add(_cells[col][row + 1]);
        }
        
        if (row - 1 >= 0 && _cells[col][row - 1].isVisited)
        {
            output.Add(_cells[col][row - 1]);
        }
        
        if (col - 1 >= 0 && _cells[col - 1][row].isVisited)
        {
            output.Add(_cells[col - 1][row]);
        }
        
        if (col + 1 < _mapConfig.mapSize && _cells[col + 1][row].isVisited)
        {
            output.Add(_cells[col + 1][row]);
        }
        
        return output;
    }

}