using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public StartGameManager startGameManager;
    public EndGameManager endGameManager;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }

        startGameManager.Initialize();
        endGameManager.Initialize();
        
        startGameManager.StartGame();
    }
}