using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject nextButton, previousButton;
    [SerializeField] private GameObject carLocation;
    
    private int _currentCarIndex;
    private GameObject _currentCarInstance;

    void Start()
    {
        _currentCarIndex = PlayerPrefs.GetInt("carIndex");

        if (_currentCarIndex == null)
        {
            _currentCarIndex = 0;
            PlayerPrefs.SetInt("carIndex", _currentCarIndex);
            PlayerPrefs.Save();
        }

        UpdateButtonDisplay();
        SpawnCar();
    }
    
    public void PressNext()
    {
        _currentCarIndex++;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", _currentCarIndex);
        PlayerPrefs.Save();
        UpdateButtonDisplay();
    }
    
    public void PressPrevious()
    {
        _currentCarIndex--;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", _currentCarIndex);
        PlayerPrefs.Save(); 
        UpdateButtonDisplay();
    }
    
    private void UpdateButtonDisplay()
    {
        nextButton.SetActive(_currentCarIndex < cars.Length - 1);
        previousButton.SetActive(_currentCarIndex > 0);
    }

    private void SpawnCar()
    {
        if (_currentCarInstance != null)
        {
            Destroy(_currentCarInstance);
        }
        _currentCarInstance = Instantiate(cars[_currentCarIndex], carLocation.transform.position, carLocation.transform.rotation);
    }
    
    public void LaunchGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
