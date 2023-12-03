using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CarSelection : MonoBehaviour
{

    public GameObject[] cars;
    public Button next;
    public Button prev;
    private int index;
    [SerializeField] private GameObject _carLocation;

    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex");
        for (int i = 0; i < cars.Length; i++)
        {
            //Instantiate(cars[index], _carLocation.transform.position, Quaternion.identity);
            //cars[i].SetActive(false);
        }
        index = 0;

        if (index == null)
        {
            index = 0;
            PlayerPrefs.SetInt("carIndex", index);
            PlayerPrefs.Save();
        }
        //cars[index].SetActive(true);
        Instantiate(cars[index], _carLocation.transform.position, Quaternion.identity);
    }


    private void Update()
    {
        if (index >= cars.Length)
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
            next.interactable = true;
        }
    }


    public void Next()
    {
        index++;
        for (int i = 0; i < cars.Length; i++)
        {
            Instantiate(cars[index], _carLocation.transform.position, Quaternion.identity);
            //cars[i].SetActive(false);
        }
        Instantiate(cars[index], _carLocation.transform.position, Quaternion.identity);
        //cars[index].SetActive(true);
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }


    public void Prev()
    {
        index--;
        for (int i = 0; i < cars.Length; i++)
        {
            //cars[i].SetActive(false);
        }
        Instantiate(cars[index], _carLocation.transform.position, Quaternion.identity);
        //cars[index].SetActive(true);
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save(); 
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Merge");
    }
}
