using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private ManagerCar _managerCar;
    void Update()
    {

        if (Input.GetKey(KeyCode.Z) && !(Input.GetKey(KeyCode.S)))
        {
            _managerCar.MoveCarStraightDirection();
        }
        
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.Z)))
        {
            _managerCar.ReverseCar();
        }
        
        if (Input.GetKey(KeyCode.Z) && (Input.GetKey(KeyCode.S)))
        {
            _managerCar.CancelCarAccelaration();
        }
        
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            _managerCar.MoveCarRightDirection(Input.GetAxis("Horizontal"));
        }
        
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            _managerCar.MoveCarLeftDirection(Input.GetAxis("Horizontal"));
        }
        
        else
        {
            _managerCar.Decelerate();
        }
        
    }
}