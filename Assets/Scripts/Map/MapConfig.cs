using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Config/Map", order = 1)]
public class MapConfig : ScriptableObject
{
    [Header("MAP")]
    [Tooltip("Lenght of a square map")]
    public int mapSize;
    [Tooltip("Number of cell that will compose the final circuit")]
    public int circuitSize;

    public int sizeScaler = 1;
}