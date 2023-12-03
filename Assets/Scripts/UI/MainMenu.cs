using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditMenu;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("SelectCar");
    }

    public void OpenSettingsMenu()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
    
    public void OpenCreditMenu()
    {
        _creditMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void CloseCreditMenu()
    {
        _creditMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
