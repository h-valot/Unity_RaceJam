using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject leftWall, rightWall, frontWall, backWall;

    [HideInInspector] public PointType type = PointType.MID;
    
    public void OnCollisionEnter(Collision other)
    {
        // exit, if the collided gameobject isn't the car
        ManagerCar car = other.collider.GetComponentInChildren<ManagerCar>();
        if (car == null) return;

        // exit, if the cell isn't the last one
        if (type != PointType.END) return;
        
        
    }

    public void ClearLeftWall() => leftWall.SetActive(false);
    public void ClearRightWall() => rightWall.SetActive(false);
    public void ClearFrontWall() => frontWall.SetActive(false);
    public void ClearBackWall() => backWall.SetActive(false);
}