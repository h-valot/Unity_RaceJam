using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public TimeManager timeManager;
    
    private void Start()
    {
        mapManager.GenerateMap();
        SpawnPlayerCar();
        SpawnAIs();
        timeManager.StartTimer();
    }

    private void SpawnPlayerCar()
    {
        
    }

    private void SpawnAIs()
    {
        
    }
}