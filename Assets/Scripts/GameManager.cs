using Script.AI.Car;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public TimeManager timeManager;
    public GameObject playerCarPrefab;    
    public GameObject aiPrefab;

    [Header("GAME SETTINGS")]
    public int aiAmount = 4;
    
    private void Start()
    {
        mapManager.GenerateMap();
        SpawnPlayerCar();
        // SpawnAIs();
        timeManager.StartTimer();
    }

    private void SpawnPlayerCar()
    {
        var playerCar = Instantiate(playerCarPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        playerCar.transform.eulerAngles = mapManager.currentMap.GetStartOrientation();
    }

    private void SpawnAIs()
    {
        for (int i = 0; i < aiAmount; i++)
        {
            var newAI = Instantiate(aiPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform).GetComponent<AICar>();
            var targetTransform = mapManager.currentMap.GetFinishCellTransform();
            newAI.Initialize(targetTransform);
        }
    }
}