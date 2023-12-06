using Car;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GarageUIManager : MonoBehaviour
    {
        [Header("REFERENCES")]
        public GameObject nextButton;
        public GameObject previousButton;

        private CarModel _carModel;
    
        public void Initialize(CarModel carModel)
        {
            _carModel = carModel;
            UpdateButtonDisplay();
        }
    
        public void PressNext()
        {
            DataManager.data.carMaterialIndex++;
            _carModel.UpdateMaterial(DataManager.data.carMaterialIndex);
            UpdateButtonDisplay();
        }
    
        public void PressPrevious()
        {
            DataManager.data.carMaterialIndex--;
            _carModel.UpdateMaterial(DataManager.data.carMaterialIndex);
            UpdateButtonDisplay();
        }
    
        /// <summary>
        /// Hides buttons to avoid out of bounds exceptions
        /// </summary>
        private void UpdateButtonDisplay()
        {
            nextButton.SetActive(DataManager.data.carMaterialIndex < Registry.gameConfig.playerCarMaterials.Length - 1);
            previousButton.SetActive(DataManager.data.carMaterialIndex > 0);
        }
    
        public void LaunchGameScene()
        {
            DataManager.Save();
            SceneManager.LoadScene("MainMenu");
        }
    }
}