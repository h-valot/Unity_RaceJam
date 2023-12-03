using Data;
using Map;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public TimeManager timeManager;
    public TextMeshProUGUI scoreTM;
    
    [Header("SCORE SETTINGS")] 
    [Range(1f, 10f)] public float scoreMultiplier;

    public void AddScore()
    {
        int pointEarned = Mathf.RoundToInt(Registry.mapConfig.circuitSize * scoreMultiplier - timeManager.currentTime);
        DataManager.data.score += pointEarned;
        DisplayScore();
    }

    public void DisplayScore() => scoreTM.text = $"Score: {DataManager.data.score}";
}