using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public MapConfig mapConfig;
    public MapView mapView;
    
    private Map _currentMap;
    
    private void Start()
    {
        GenerateNewMap();
    }

    private void GenerateNewMap()
    {
        _currentMap = MapGenerator.GetMap(mapConfig);
        mapView.ShowMap(_currentMap);
    }
}