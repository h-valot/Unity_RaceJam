using Map;
using Script.AI.Car;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public TimeManager timeManager;
    public GameObject playerCarPrefab;    
    public GameObject aiPrefab;

    [Header("GAME SETTINGS")]
    public int aiAmount = 4;
    public float scoreMultiplier = 4;
    
    private void Start()
    {
        mapManager.GenerateMap();
        SpawnPlayerCar();
        // SpawnAIs();
        timeManager.StartTimer();
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
        for (int i = 0; i < aiAmount; i++)
        {
            var newAI = Instantiate(aiPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform).GetComponent<AICar>();
            var targetTransform = mapManager.currentMap.GetFinishCellTransform();
            newAI.Initialize(targetTransform);
        }
    }

    private void OnEnable()
    {
        Events.onPlayerReachesEnd += AddScore;
        Events.onPlayerReachesEnd += Reload;
    }

    private void OnDisable()
    {
        Events.onPlayerReachesEnd -= AddScore;
        Events.onPlayerReachesEnd -= Reload;
    }

    private void AddScore()
    {
        int pointEarned = Mathf.RoundToInt(mapManager.mapConfig.circuitSize * scoreMultiplier - timeManager.currentTime);
        Debug.Log($"GAME MANAGER: +{pointEarned} point(s)");
    }

    private void Reload()
    {
        // handle end animations
        
        // load game scene
        Debug.Log($"GAME MANAGER: reloading {SceneManager.GetActiveScene().name} scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}