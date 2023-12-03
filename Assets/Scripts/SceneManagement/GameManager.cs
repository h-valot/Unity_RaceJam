using Data;
using Map;
using Script.AI.Car;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public ScoreManager scoreManager;
    public GameObject playerCarPrefab;    
    public GameObject aiPrefab;
    public Transform[] aiSpawnPoints;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }
        
        mapManager.GenerateMap();
        SpawnPlayerCar();
        SpawnAIs();
        scoreManager.Initialize();
    }

    /// <summary>
    /// Instantiate the player's car and make it face the correct direction
    /// </summary>
    private void SpawnPlayerCar()
    {
        var playerCar = Instantiate(playerCarPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        playerCar.transform.eulerAngles = mapManager.currentMap.GetStartOrientation();
    }

    private void SpawnAIs()
    {
        for (int i = 0; i < Registry.gameConfig.aiAmount; i++)
        {
            var aiPos = new Vector3(aiSpawnPoints[i].position.x * Registry.mapConfig.sizeScaler/2, 0.5f, aiSpawnPoints[i].position.z * Registry.mapConfig.sizeScaler/2);
            var newAI = Instantiate(aiPrefab, aiPos, Quaternion.identity, transform).GetComponent<AICar>();
            var targetTransform = mapManager.currentMap.GetFinishCellTransform();
            newAI.Initialize(targetTransform);
        }
    }

    private void OnEnable()
    {
        Events.onPlayerReachesEnd += scoreManager.AddScore;
        Events.onPlayerReachesEnd += Reload;
    }

    private void OnDisable()
    {
        Events.onPlayerReachesEnd -= scoreManager.AddScore;
        Events.onPlayerReachesEnd -= Reload;
    }
    
    private void Reload()
    {
        // save data
        DataManager.data.raceAmount++;
        DataManager.Save();
        
        if (DataManager.data.raceAmount < Registry.gameConfig.raceAmount)
        {
            // reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // save highest score
            if (DataManager.data.score > DataManager.data.highestScore) 
                DataManager.data.highestScore = DataManager.data.score;
            
            // load main menu scene
            SceneManager.LoadScene("MainMenu");
        }
        
    }
}