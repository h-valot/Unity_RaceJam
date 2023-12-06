using Data;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    [Header("CONFIGS")]
    public MapConfig mapConfig;
    public GameConfig gameConfig;

    [Header("REFERENCES")]
    public InitLoadingScreenUIManager initLoadingScreenUIManager;
    
    private async void Start()
    {
        // synchronize scriptable objects
        Registry.mapConfig = mapConfig;
        Registry.gameConfig = gameConfig;

        // load persistant data from cache
        DataManager.Load();

        await initLoadingScreenUIManager.AnimateLoadingScreen();
        
        Registry.isInitialized = true;
        SceneManager.LoadScene(gameConfig.startingScene);
    }
}