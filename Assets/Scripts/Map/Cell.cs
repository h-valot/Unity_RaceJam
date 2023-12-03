using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject leftWall, rightWall, frontWall, backWall;
    public Vector2 pos;
    
    [HideInInspector] public Cell next;
    [HideInInspector] public Cell previous;
    [HideInInspector] public int place;
    [HideInInspector] public bool isVisited;s
    [HideInInspector] public CellType type = CellType.MID;
    
    public void Visit() => isVisited = true;

    public void SetType(CellType type) => this.type = type;

    public void OnCollisionEnter(Collision other)
    {
        // exit, if the collided gameobject isn't the car
        ManagerCar car = other.collider.GetComponentInChildren<ManagerCar>();
        if (car == null) return;

        // exit, if the cell isn't the last one
        if (type != CellType.END) return;
        
        // 
    }

    public void Hide()
    {
        ClearRightWall();
        ClearLeftWall();
        ClearFrontWall();
        ClearBackWall();
    }

    public void ClearLeftWall() => leftWall.SetActive(false);
    public void ClearRightWall() => rightWall.SetActive(false);
    public void ClearFrontWall() => frontWall.SetActive(false);
    public void ClearBackWall() => backWall.SetActive(false);
}

public enum CellType
{
    START = 0,
    MID,
    END
}