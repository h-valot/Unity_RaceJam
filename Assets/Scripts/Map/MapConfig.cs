using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Config/Map", order = 1)]
public class MapConfig : ScriptableObject
{
    [Header("MAP")]
    [Tooltip("Lenght of a square map")]
    public int mapSize;

    [Header("NODES")]
    [Tooltip("Percentage of a node to be available")]
    [Range(0f, 1f)] public float availabilityNodeAmount;
}