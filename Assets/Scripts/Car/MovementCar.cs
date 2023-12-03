using UnityEngine;
using System;
using System.Collections.Generic;

public class MovementCar : MonoBehaviour
{

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    [SerializeField] private float maxAcceleration = 30.0f;
    [SerializeField] private float brakeAcceleration = 50.0f;

    [SerializeField] private float turnSensitivity;
    [SerializeField] private float maxSteerAngle;


    [SerializeField] private List<Wheel> wheels;

    private float moveInput;
    private float steerInput;
    private Rigidbody carRb;


    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    private void Update()
    {
        AnimateWheels();
    }

    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    

    public void ReceiveInput(float horizontalInput, float verticalInput)
    {
        moveInput = verticalInput;
        steerInput = horizontalInput;
    }

    
    
    // Move Car
    private void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    
    
    // Turn the car
    private void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    // Stop the car
    private void Brake()
    {
        if (moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                // Brake the car
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }

        }
        else
        {
            foreach (var wheel in wheels)
            {
                // Release the brakes
                wheel.wheelCollider.brakeTorque = 0;
            }

        }
    }

    
    // Make the animation of the car wheels
    private void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}