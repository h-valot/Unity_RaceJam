using Car;
using Data;
using Map;
using AI.Car;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")] 
    public MapManager mapManager;
    public ScoreManager scoreManager;
    public Transform[] aiSpawnPoints;
    
    private bool _doHandleEnd;
    
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
    /// Instantiate the player's car with the desired skin and make it face the correct direction
    /// </summary>
    private void SpawnPlayerCar()
    {
        var carMaterialIndex = PlayerPrefs.GetInt("carIndex");
        var playerCar = Instantiate(Registry.gameConfig.playerCarPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, transform);
        playerCar.transform.eulerAngles = mapManager.currentMap.GetStartOrientation();
        playerCar.GetComponentInChildren<CarManager>().carGraphics.UpdateMaterial(carMaterialIndex);
    }

    /// <summary>
    /// Spawn AI cars next to the player
    /// </summary>
    private void SpawnAIs()
    {
        for (int i = 0; i < Registry.gameConfig.aiAmount; i++)
        {
            var aiPos = new Vector3(aiSpawnPoints[i].position.x * Registry.mapConfig.sizeScaler/2, 0.5f, aiSpawnPoints[i].position.z * Registry.mapConfig.sizeScaler/2);
            var newAI = Instantiate(Registry.gameConfig.aiCarPrefab, aiPos, Quaternion.identity, transform).GetComponent<AICar>();
            newAI.UpdateTarget(mapManager.currentMap.GetNextCellTransform(0, 1));
            newAI.SetSpeed();
        }
    }

    private void OnEnable()
    {
        Events.onCircuitEnded += HandleEnd;
    }

    private void OnDisable()
    {
        Events.onCircuitEnded -= HandleEnd;
    }

    /// <summary>
    /// Manages end game and load a new game or menu scene
    /// </summary>
    private async void HandleEnd(bool wallHitten)
    {
        // cannot enter this fonction several times
        if (_doHandleEnd) return;
        _doHandleEnd = true;

        scoreManager.HandleEnd(wallHitten);
        
        // scene management
        DataManager.data.raceAmount++;
        if (DataManager.data.raceAmount < Registry.gameConfig.raceAmount)
        {
            await scoreManager.AnimateEndCircuit(wallHitten);
            
            // reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // save highest score
            if (DataManager.data.score > DataManager.data.highestScore)
                DataManager.data.highestScore = DataManager.data.score;
            DataManager.Save();
            
            await scoreManager.AnimateEndCycle();
            
            // load main menu scene
            SceneManager.LoadScene("MainMenu");
        }
    }

}