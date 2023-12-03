using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("REFERENCE")] 
    public MapConfig mapConfig;
    public MapView mapView;
    
    public Map currentMap;
    
    public void GenerateMap()
    {
        currentMap = MapGenerator.GetMap(mapConfig);
        mapView.ShowMap(currentMap, mapConfig);
    }
}
