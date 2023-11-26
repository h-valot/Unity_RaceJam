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
            for (int y = 0; y < _mapConfig.mapSize; y++)
            {
                // randomly set their availability
                _cells[x][y] = new Cell(x, y);
            }
        }
    }

    private static List<Cell> GeneratePath()
    {
        // select the starting and the ending cell based on settings in config
        
        // generate a* path to get the list of cell from the starting to the ending node
        // the a* must avoid unavailable cells 
        // if it can't find a way thought cells, regenerate cells

        return new List<Cell>();
    }
}