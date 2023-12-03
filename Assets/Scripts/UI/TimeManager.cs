using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public TextMeshProUGUI timerTM;
    
    [HideInInspector] public float currentTime;
    private bool _canRunTimer;
    
    private void OnEnable()
    {
        Events.onPlayerReachesEnd += StopTimer;
    }

    private void OnDisable()
    {
        Events.onPlayerReachesEnd -= StopTimer;
    }
    
    public void StartTimer() => _canRunTimer = true;
    private void StopTimer() => _canRunTimer = false;

    private void Update()
    {
        if (!_canRunTimer) return;
        
        UpdateTimer();
        DisplayTime();
    }

    private void UpdateTimer()
    {
        currentTime += Time.deltaTime;
    }
    
    private void DisplayTime()
    {
        timerTM.text = $"{Mathf.CeilToInt(currentTime)}s";
    }
}