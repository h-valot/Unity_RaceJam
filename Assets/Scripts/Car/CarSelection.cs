using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CarSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Button next;
    [SerializeField] private Button prev;
    [SerializeField] private GameObject _carLocation;
    
    private int _index;
    private GameObject _currentCarInstance;

    void Start()
    {
        _index = PlayerPrefs.GetInt("carIndex");

        if (_index == null)
        {
            _index = 0;
            PlayerPrefs.SetInt("carIndex", _index);
            PlayerPrefs.Save();
        }

        SpawnCar();
    }
    
    private void Update()
    {
        if (_index >= cars.Length-1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }

        if (_index <= 0)
        {
            prev.interactable = false;
        }
        else
        {
            prev.interactable = true;
        }
    }
    
    public void Next()
    {
        _index++;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", _index);
        PlayerPrefs.Save();
    }
    
    public void Prev()
    {
        _index--;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", _index);
        PlayerPrefs.Save(); 
    }

    private void SpawnCar()
    {
        if (_currentCarInstance != null)
        {
            Destroy(_currentCarInstance);
        }
        _currentCarInstance = Instantiate(cars[_index], _carLocation.transform.position, _carLocation.transform.rotation);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
