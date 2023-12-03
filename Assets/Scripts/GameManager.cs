using Script.AI.Car;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public TimeManager timeManager;

    public int AIAmount = 4;
    public GameObject AIPrefab;
    
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
        for (int i = 0; i < AIAmount; i++)
        {
            var newAI = Instantiate(AIPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform).GetComponent<AICar>();
            var targetTransform = mapManager.currentMap.GetFinishCellTransform();
            newAI.Initialize(targetTransform);
        }
    }
}