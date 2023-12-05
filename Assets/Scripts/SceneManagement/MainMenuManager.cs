using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public MainMenuUIManager mainMenuUIManager;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }
        
        mainMenuUIManager.Initialize();
    }
}