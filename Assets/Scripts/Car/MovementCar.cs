//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class MovementCar : MonoBehaviour
//{
//    
//    [SerializeField] private float _acceleration = 0.1f;
//    [SerializeField] private float _deceleration = 0.05f;
//    [SerializeField] private float _maxSpeed = 160f;
//    private float _currentSpeed = 0f;
//
//
//    public float CurrentSpeed
//    {
//        get => _currentSpeed;
//    }
//
//    public void Accelerate()
//    {
//        _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _maxSpeed);
//    }
//
//    public void Decelerate()
//    {
//        _currentSpeed = Mathf.Max(_currentSpeed - _deceleration, 0);
//    }
//
//    public void MoveStraight()
//    {
//        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
//    }
//
//
//    public void MoveOnRight(float horizontalInput)
//    {
//        transform.Rotate(0, horizontalInput * (_currentSpeed / 2) * Time.deltaTime, 0);
//    }
//
//    public void MoveOnLeft(float horizontalInput)
//    {
//        transform.Rotate(0,  (horizontalInput * (_currentSpeed / 2) * Time.deltaTime), 0);
//    }
//
//    public void ReverseCar()
//    {
//        float tmpMaxSpeed = _maxSpeed / 2;
//        _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, tmpMaxSpeed);
//        transform.Translate(Vector3.back * _currentSpeed * Time.deltaTime);
//    }
//
//}


//using UnityEngine;
//
//[RequireComponent(typeof(Rigidbody))]
//public class MovementCar : MonoBehaviour
//{
//    public float accelerationForce = 800f;  // Force d'accélération de la voiture
//    public float turnStrength = 500f;         // Force de rotation
//    public float maxSpeed = 20f;            // Vitesse maximale
//    private Rigidbody rb;                   // Référence au Rigidbody
//    [SerializeField] private float horizontalInput;          // Input horizontal
//    [SerializeField] private float verticalInput;  
//    
//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//    }
//
//    public void ReceiveInput(float horizontal, float vertical)
//    {
//        horizontalInput = horizontal;
//        verticalInput = vertical;
//    }
//
//    private void FixedUpdate()
//    {
//        //// Gère l'accélération et la décélération
//        //if (verticalInput != 0)
//        //{
//        //    // Applique une force en avant ou en arrière en fonction de l'input vertical
//        //    rb.AddForce(transform.forward * verticalInput * acceleration, ForceMode.Acceleration);
//        //} 
//        //else
//        //{
//        //    // Aucun input vertical : décélération automatique (peut être ajusté avec le Drag du Rigidbody)
//        //}
////
//        //// Limite la vitesse maximale
//        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
////
//        //// Application de la rotation
//        //if (horizontalInput != 0)
//        //{
//        //    // Calcule le nouveau vecteur de rotation
//        //    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, horizontalInput * turnSpeed * Time.deltaTime, 0));
//        //    // Applique la rotation au Rigidbody
//        //    rb.MoveRotation(rb.rotation * deltaRotation);
//        //}
//        
//        // Appliquer une force d'accélération seulement si l'input vertical est reçu (avancer/reculer)
//        // et que la vitesse actuelle est inférieure à la vitesse maximale au carré (pour éviter de dépasser la vitesse maximale).
//        if (Mathf.Abs(verticalInput) > 0 && rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
//        {
//            rb.AddForce(transform.forward * verticalInput * accelerationForce, ForceMode.Force);
//        }
//
//        // Appliquer un couple pour la rotation autour de l'axe Y de la voiture.
//        // Ce couple dépend de l'input horizontal (tourner à gauche/droite).
//        if (horizontalInput != 0)
//        {
//            rb.AddTorque(new Vector3(0f, horizontalInput * turnStrength, 0f), ForceMode.Force);
//        }
//
//        // Limiter la vitesse du véhicule à la valeur maxSpeed pour éviter qu'il n'accélère indéfiniment.
//        // Utiliser ClampMagnitude pour maintenir la direction de la vitesse tout en limitant sa magnitude.
//        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
//    }
//    
//    
//    
//    
//}



using UnityEngine;
using System;
using System.Collections.Generic;

public class MovementCar : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

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

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    //public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;


    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        //carRb.centerOfMass = _centerOfMass;
        carRb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
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

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }

        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

        }
    }

    void AnimateWheels()
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