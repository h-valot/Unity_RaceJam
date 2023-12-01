//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class ManagerCar : MonoBehaviour
//{
//
//    [SerializeField] private MovementCar _movementCar;
//    [SerializeField] private WheelsManager _wheelsManager;
//
//    public void MoveCarStraightDirection()
//    {
//        _movementCar.Accelerate();
//        _movementCar.MoveStraight();
//    }
//
//    public void MoveCarRightDirection(float horizontalInput)
//    {
//        _movementCar.MoveOnRight(horizontalInput);
//    }
//
//
//    public void MoveCarLeftDirection(float horizontalInput)
//    {
//        _movementCar.MoveOnLeft(horizontalInput);
//    }
//
//    public void ReverseCar()
//    {
//        _movementCar.ReverseCar();
//    }
//    
//    public void CancelCarAccelaration()
//    {
//        _movementCar.Decelerate();
//    }
//
//    public void Decelerate()
//    {
//        _movementCar.Decelerate();
//    }
//    
//    
//}


using UnityEngine;

public class ManagerCar : MonoBehaviour
{
    private InputPlayer inputPlayer;
    private MovementCar movementCar;

    void Start()
    {
        inputPlayer = GetComponent<InputPlayer>();
        movementCar = GetComponent<MovementCar>();
    }

    void Update()
    {
        //movementCar.ReceiveInput(inputPlayer.horizontalInput, inputPlayer.verticalInput);
    }
}
