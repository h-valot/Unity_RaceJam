using System.Collections.Generic;
using UnityEngine;

public static class MapGenerator
{
    private static List<List<Node>> _nodes = new List<List<Node>>();
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
        
        // generate all nodes
        GenerateNodes();
        
        // a* pathfinding
        var path = GeneratePath();

        // returns a list with nodes that form the circuit
        return new Map(path);
    }

    private static void GenerateNodes()
    {
        for (int x = 0; x < _mapConfig.mapSize; x++)
        {
            for (int y = 0; y < _mapConfig.mapSize; y++)
            {
                _nodes[x][y] = new Node();
                // randomly set their availability
            }
        }
    }

    private static List<Node> GeneratePath()
    {
        // select the starting and the ending node based on settings in config
        
        // generate a* path to get the list of node from the starting to the ending node
        // the a* must avoid unavailable nodes 
        // if it can't find a way thought nodes, regenerate nodes

        return new List<Node>();
    }
}