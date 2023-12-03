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
        // SpawnAIs();
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
            var newAI = Instantiate(aiPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform).GetComponent<AICar>();
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
        // handle end animations
        
        // save data
        DataManager.data.raceAmount++;
        DataManager.Save();
        
        if (DataManager.data.raceAmount < Registry.gameConfig.raceAmount)
        {
            // load game scene
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