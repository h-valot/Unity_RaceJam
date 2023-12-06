using AI.Car;
using Car;
using Map;
using Score;
using UI;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public ScoreManager scoreManager;
    public Transform[] aiSpawnPoints;
    
    [Header("EXTERNAL REFERENCES")]
    public MapManager mapManager;
    public StartGameUIManager startGameUIManager;

    public void Initialize()
    {
        mapManager.GenerateMap();
        SpawnPlayerCar();
        SpawnAIs();
        scoreManager.Initialize();
    }

    public void StartGame()
    {        
        startGameUIManager.Initialize();
        
        Time.timeScale = 0f;
        startGameUIManager.AnimateStart();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Instantiate the player's car with the desired skin and make it face the correct direction
    /// </summary>
    private void SpawnPlayerCar()
    {
        var playerCar = Instantiate(Registry.gameConfig.playerCarPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        playerCar.transform.eulerAngles = mapManager.currentMap.GetStartOrientation();
        playerCar.GetComponentInChildren<CarManager>().carGraphics.UpdateMaterial();
    }

    /// <summary>
    /// Spawn AI cars next to the player
    /// </summary>
    private void SpawnAIs()
    {
        for (int i = 0; i < Registry.gameConfig.aiAmount; i++)
        {
            var aiPos = new Vector3(aiSpawnPoints[i].position.x * Registry.mapConfig.sizeScaler/2, 0f, aiSpawnPoints[i].position.z * Registry.mapConfig.sizeScaler/2);
            var newAI = Instantiate(Registry.gameConfig.aiCarPrefab, aiPos, Quaternion.identity, transform).GetComponent<AICar>();
            newAI.UpdateTarget(mapManager.currentMap.GetNextCellTransform(0, 1));
            newAI.SetSpeed();
        }
    }
}