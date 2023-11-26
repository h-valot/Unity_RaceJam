using System.Collections.Generic;
using UnityEngine;

public static class MapGenerator
{
    private static List<List<Cell>> _cells = new List<List<Cell>>();
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
        for (int x = 0; x < _mapConfig.mapSize; x++)
        {
            _cells.Add(new List<Cell>());
            for (int y = 0; y < _mapConfig.mapSize; y++)
            {
                bool isAvailable = Random.Range(0f, 1f) <= _mapConfig.availabilityPercentage;
                _cells[x].Add(new Cell(x, y, isAvailable));
            }
        }
    }

    private static List<Cell> GeneratePath()
    {
        // select the starting and the ending cell based on settings in config
        tryAmount = 0;
        Cell startingCell = GetStartingCell();
        tryAmount = 0;
        Cell endingCell = GetEndingCell();
        
        Debug.Log("MAP GENERATOR:" +
                  $"\r\nStarting cell {startingCell.x}, {startingCell.y} availability {startingCell.isAvailable}" +
                  $"\r\nEnding cell {endingCell.x}, {endingCell.y} availability {endingCell.isAvailable}");
        
        // generate a* path to get the list of cell from the starting to the ending node
        // the a* must avoid unavailable cells 
        // if it can't find a way thought cells, regenerate cells

        return new List<Cell>();
    }

    private static int tryAmount;
    private static Cell GetStartingCell()
    {
        tryAmount++;
        if (tryAmount >= _mapConfig.mapSize * _mapConfig.mapSize)
        {
            Debug.LogError("MAP GENERATOR: all cells are unavailable");
            return null;
        }
        
        Cell output = _cells[0 + Mathf.FloorToInt((float)tryAmount / (float)_mapConfig.mapSize)][Random.Range(0, _mapConfig.mapSize - 1)];
        if (!output.isAvailable) output = GetStartingCell();

        return output;
    }

    private static Cell GetEndingCell()
    {
        tryAmount++;
        if (tryAmount >= _mapConfig.mapSize * _mapConfig.mapSize)
        {
            Debug.LogError("MAP GENERATOR: all cells are unavailable");
            return null;
        }
        
        Cell output = _cells[_mapConfig.mapSize - 1 - Mathf.FloorToInt((float)tryAmount / (float)_mapConfig.mapSize)][Random.Range(0, _mapConfig.mapSize - 1)];
        if (!output.isAvailable) output = GetEndingCell();

        return output;
    }
}