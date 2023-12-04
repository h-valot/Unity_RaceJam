using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CarSelection : MonoBehaviour
{

    [SerializeField] private GameObject[] cars;
    [SerializeField] private Button next;
    [SerializeField] private Button prev;
    [SerializeField] private GameObject _carLocation;
    private int index;
    private GameObject currentCarInstance;

    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex");

        if (index == null)
        {
            index = 0;
            PlayerPrefs.SetInt("carIndex", index);
            PlayerPrefs.Save();
        }

        SpawnCar();
    }
    
    private void Update()
    {
        if (index >= cars.Length-1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }

        if (index <= 0)
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
        index++;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }
    
    public void Prev()
    {
        index--;
        SpawnCar();
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save(); 
    }

    private void SpawnCar()
    {
        if (currentCarInstance != null)
        {
            Destroy(currentCarInstance);
        }
        currentCarInstance = Instantiate(cars[index], _carLocation.transform.position, _carLocation.transform.rotation);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Map");
    }
}
