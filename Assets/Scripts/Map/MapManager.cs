using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public MapConfig mapConfig;
    
    private Map _currentMap;
    
    private void Start()
    {
        GenerateNewMap();
    }

    private void GenerateNewMap()
    {
        _currentMap = MapGenerator.GetMap(mapConfig);
    }
}