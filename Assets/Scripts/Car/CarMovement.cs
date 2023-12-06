using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class CarMovement : MonoBehaviour
    {
        public enum Axel
        {
            FRONT,
            REAR
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

        private float _moveInput;
        private float _steerInput;
        private Rigidbody _carRigidbody;

        private void Start()
        {
            _carRigidbody = GetComponent<Rigidbody>();
            _carRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
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

        /// <summary>
        /// Gathers player inputs
        /// </summary>
        public void ReceiveInput(float horizontalInput, float verticalInput)
        {
            _moveInput = verticalInput;
            _steerInput = horizontalInput;
        }
    
        /// <summary>
        /// Move the car
        /// </summary>
        private void Move()
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = _moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
        }
    
        /// <summary>
        /// Turns the car
        /// </summary>
        private void Steer()
        {
            foreach(var wheel in wheels)
            {
                if (wheel.axel == Axel.FRONT)
                {
                    var _steerAngle = _steerInput * turnSensitivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
                }
            }
        }

        /// <summary>
        /// Stops the car's movement
        /// </summary>
        private void Brake()
        {
            if (_moveInput == 0)
            {
                foreach (var wheel in wheels)
                {
                    // brake the car
                    wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
                }

            }
            else
            {
                foreach (var wheel in wheels)
                {
                    // release the brakes
                    wheel.wheelCollider.brakeTorque = 0;
                }

            }
        }
    
        /// <summary>
        /// Animates the car wheels
        /// </summary>
        private void AnimateWheels()
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.GetWorldPose(out var position, out var rotation);
                wheel.wheelModel.transform.position = position;
                wheel.wheelModel.transform.rotation = rotation;
            }
        }
    }
}