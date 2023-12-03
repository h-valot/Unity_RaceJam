using Data;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    public MapConfig mapConfig;
    public GameConfig gameConfig;
    
    private void Start()
    {
        var asyncOperation = SceneManager.LoadSceneAsync(gameConfig.startingScene);
        
        // synchronize scriptable objects
        Registry.mapConfig = mapConfig;
        Registry.gameConfig = gameConfig;

        // load persistant data from cache
        DataManager.Load();
        
        Registry.isInitialized = true;
        asyncOperation.allowSceneActivation = true;
    }
}