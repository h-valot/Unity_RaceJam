using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCar : MonoBehaviour
{
    
    [SerializeField] private float _acceleration = 0.1f;
    [SerializeField] private float _deceleration = 0.05f;
    [SerializeField] private float _maxSpeed = 160f;
    private float _currentSpeed = 0f;
    
    
    public void Accelerate()
    {
        _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _maxSpeed);
    }

    public void Decelerate()
    {
        _currentSpeed = Mathf.Max(_currentSpeed - _deceleration, 0);
    }

    public void MoveStraight()
    {
        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
    }


    public void MoveOnRight(float horizontalInput)
    {
        transform.Rotate(0, horizontalInput * (_currentSpeed / 2) * Time.deltaTime, 0);
    }

    public void MoveOnLeft(float horizontalInput)
    {
        transform.Rotate(0,  (horizontalInput * (_currentSpeed / 2) * Time.deltaTime), 0);
    }

    public void ReverseCar()
    {
        float tmpMaxSpeed = _maxSpeed / 2;
        _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, tmpMaxSpeed);
        transform.Translate(Vector3.back * _currentSpeed * Time.deltaTime);
    }

}
