using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Config/Map", order = 1)]
public class MapConfig : ScriptableObject
{
    [Header("MAP")]
    [Tooltip("Lenght of a square map")]
    public int mapSize;

    [Header("PATH")] 
    [Tooltip("Lenght of the path can not be small than this value")]
    public int minPathSize;
    [Tooltip("Maximum number of tries to generate the path allowed before aborting")]
    public int maxGenerationTry;
    
    [Header("CELLS")]
    [Tooltip("Percentage of a cell to be available")]
    [Range(0.5f, 1f)] public float availabilityPercentage;
}