using Car;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private GameObject nextButton, previousButton;
    [SerializeField] private GameObject carLocation;
    
    private int _currentCarIndex;
    private CarModel _currentCarInstance;

    void Start()
    {
        _currentCarIndex = PlayerPrefs.GetInt("carIndex");
        UpdateButtonDisplay();
        SpawnCar();
    }
    
    public void PressNext()
    {
        _currentCarIndex++;
        _currentCarInstance.UpdateMaterial(_currentCarIndex);
        PlayerPrefs.SetInt("carIndex", _currentCarIndex);
        PlayerPrefs.Save();
        UpdateButtonDisplay();
    }
    
    public void PressPrevious()
    {
        _currentCarIndex--;
        _currentCarInstance.UpdateMaterial(_currentCarIndex);
        PlayerPrefs.SetInt("carIndex", _currentCarIndex);
        PlayerPrefs.Save(); 
        UpdateButtonDisplay();
    }
    
    private void UpdateButtonDisplay()
    {
        nextButton.SetActive(_currentCarIndex < Registry.gameConfig.playerCarMaterials.Length - 1);
        previousButton.SetActive(_currentCarIndex > 0);
    }

    private void SpawnCar()
    {
        if (_currentCarInstance != null) Destroy(_currentCarInstance);
        _currentCarInstance = Instantiate(Registry.gameConfig.modelCarPrefab, carLocation.transform.position, carLocation.transform.rotation).GetComponent<CarModel>();
        _currentCarInstance.UpdateMaterial(_currentCarIndex);
    }
    
    public void LaunchGameScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
