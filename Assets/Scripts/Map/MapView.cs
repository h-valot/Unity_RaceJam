using UnityEngine;

public class MapView : MonoBehaviour
{
    [Header("REFERENCE")]
    public Transform contentParent;
    
    private Map _map;
    private MapConfig _mapConfig;

    public void ShowMap(Map map, MapConfig mapConfig)
    {
        if (map == null)
        {
            Debug.LogError("MAP VIEW: this map is null");
            return;
        }

        _map = map;
        _mapConfig = mapConfig;

        GenerateCircuit();
    }
    
    
    /// <summary>
    /// Generates a circuit as a list of cell of a size based on the map config
    /// Clears walls on map to display the race circuit and hides unused cells 
    /// </summary>
    private void GenerateCircuit()
    {
        for (int index = 0; index < _map.circuit.Count; index++)
        {
            
            // clear walls of selected cells
            ClearWalls(_map.circuit[index].previous, _map.circuit[index]);
            
            // set cell placement informations
            _map.circuit[index].place = index;
            if (index == _map.circuit.Count) _map.circuit[index].SetType(CellType.END);
        }
    }
    
    
    /// <summary>
    /// Clears walls that obstruct other cells
    /// </summary>
    private static void ClearWalls(Cell previousCell, Cell currentCell)
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
}
