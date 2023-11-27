using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MapGenerator
{
    private static List<List<Cell>> _cells = new List<List<Cell>>();
    private static List<List<Cell>> _availableCells = new List<List<Cell>>();
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
        
        // generate all cells
        GenerateCells();
        
        // a* pathfinding
        var path = GeneratePath();

        // returns a list with cells that form the circuit
        return new Map(path);
    }

    private static void GenerateCells()
    {
        for (int y = 0; y < _mapConfig.mapSize; y++)
        {
            _cells.Add(new List<Cell>());
            _availableCells.Add(new List<Cell>());
            for (int x = 0; x < _mapConfig.mapSize; x++)
            {
                bool isAvailable = Random.Range(0f, 1f) <= _mapConfig.availabilityPercentage;
                if (isAvailable) _availableCells[y].Add(new Cell(x, y, true));
                _cells[y].Add(new Cell(x, y, isAvailable));
            }
        }
    }

    private static List<Cell> GeneratePath()
    {
        // select the starting and the ending cell based on settings in config
        Cell startingCell = GetStartingCell();
        Cell endingCell = GetEndingCell();

        Debug.Log("MAP GENERATOR:" +
                  $"\r\nStarting cell {startingCell.x}, {startingCell.y} availability {startingCell.isAvailable}" +
                  $"\r\nEnding cell {endingCell.x}, {endingCell.y} availability {endingCell.isAvailable}");
        
        // generate a* path to get the list of cell from the starting to the ending node
        // the a* must avoid unavailable cells 
        // if it can't find a way thought cells, regenerate cells

        return new List<Cell>();
    }

    private static Cell GetStartingCell()
    {
        Cell output = null;
        foreach (List<Cell> cells in _availableCells.AsEnumerable()!.Reverse())
        {
            foreach (Cell cell in cells.AsEnumerable().Reverse())
            {
                output = cell;
            }
        }
        return output;
    }

    private static Cell GetEndingCell()
    {
        Cell output = null;
        foreach (List<Cell> cells in _availableCells)
        {
            foreach (Cell cell in cells)
            {
                output = cell;
            }
        }
        return output;
    }
}