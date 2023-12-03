using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Config/Map", order = 1)]
public class MapConfig : ScriptableObject
{
    [Header("MAP")]
    [Tooltip("Lenght of a square map")]
    public int mapSize;
    [Tooltip("Number of cell that will compose the final circuit")]
    public int circuitSize;
    [Tooltip("Map size multiplier. Base value is 1.")]
    public int sizeScaler = 1;
    
    [Header("CELL")]
    [Tooltip("Base cell prefab")]
    public Cell cellPrefab;
}