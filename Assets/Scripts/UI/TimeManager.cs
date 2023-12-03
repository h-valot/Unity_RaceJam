using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public TextMeshProUGUI timerTM;
    
    private bool _canRunTimer;
    private float _currentTime;
    
    public void StartTimer()
    {
        _canRunTimer = true;
    }

    private void Update()
    {
        if (!_canRunTimer) return;
        
        UpdateTimer();
        DisplayTime();
    }

    private void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
    }
    
    private void DisplayTime()
    {
        timerTM.text = $"{Mathf.CeilToInt(_currentTime)}s";
    }
}