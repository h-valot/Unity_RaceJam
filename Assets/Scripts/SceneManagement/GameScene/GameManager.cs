using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public StartGameManager startGameManager;
    public EndGameManager endGameManager;
    [SerializeField] private GameObject _pauseUI;
    
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


    public void DisplayPauseGame()
    {
        if (_pauseUI.activeSelf)
        {
            _pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            _pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}