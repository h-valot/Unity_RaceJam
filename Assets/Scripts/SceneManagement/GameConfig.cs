using Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/Game", order = 1)]
public class GameConfig : ScriptableObject
{
    public string startingScene;
    
    [Header("GAME TWEAKING")]
    [Tooltip("Number of ai that will spawn in game")]
    [Range(0, 4)] public int aiAmount = 4;
    [Tooltip("Higher is this value, higher will be the player's score")]
    public float scoreMultiplier = 4;
    [Tooltip("Number of circuit played in one cycle")]
    [Range(1, 10)] public int raceAmount = 5;
    [Tooltip("Speed of AI cars")] 
    public FloatMinMax aiMovementSpeed;
}