using Data;
using Map;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public TextMeshProUGUI scoreTM;
    public TextMeshProUGUI timerTM;

    [HideInInspector] public float currentTime;
    private bool _canRunTimer;

    public void Initialize()
    {
        currentTime = Registry.mapConfig.circuitSize * Registry.gameConfig.scoreMultiplier;
        
        DisplayScore();
        StartTimer();
    }
    
    private void Update()
    {
        if (!_canRunTimer) return;
        
        currentTime -= Time.deltaTime;
        timerTM.text = $"Time: {Mathf.CeilToInt(currentTime)}s";
    }
    
    public void AddScore()
    {
        DataManager.data.score += Mathf.CeilToInt(currentTime);
        DisplayScore();
    }
    
    private void DisplayScore() => scoreTM.text = $"Score: {DataManager.data.score}";
    private void StartTimer() => _canRunTimer = true;
    private void StopTimer() => _canRunTimer = false;
    
    private void OnEnable()
    {
        Events.onPlayerReachesEnd += StopTimer;
    }

    private void OnDisable()
    {
        Events.onPlayerReachesEnd -= StopTimer;
    }
}