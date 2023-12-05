using Car;
using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageManager : MonoBehaviour
{
    [Header("REFERENCES")]
    public GameObject carLocation;
    public GarageUIManager garageUIManager;
    
    private void Start()
    {
        // exit, if registry isn't initialized
        if (!Registry.isInitialized)
        {
            SceneManager.LoadScene("Init");
            return;
        }
        
        var carModel = SpawnCar();
        garageUIManager.Initialize(carModel);
    }

    private CarModel SpawnCar()
    {
        var carModel = Instantiate(Registry.gameConfig.modelCarPrefab, carLocation.transform.position, carLocation.transform.rotation).GetComponent<CarModel>();
        carModel.UpdateMaterial(DataManager.data.carMaterialIndex);
        return carModel;
    }
}
