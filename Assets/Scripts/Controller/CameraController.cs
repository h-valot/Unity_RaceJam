using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform car; // Reference to the car's Transform
    [SerializeField] private MovementCar carMovementScript; // Reference to the car's movement script
    [SerializeField] private Vector3 offset; // Offset of the camera from the car
    [SerializeField] private float lookSpeed = 5f; // Speed at which the camera orients towards the car
    private float followResponsiveness = 0.3f;


    void LateUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    // Handles the position of the camera based on the car's position
    void HandleTranslation()
    {
        // Gets the current speed of the car from the MovementCar script
        float followSpeed = Mathf.Lerp(transform.position.magnitude, carMovementScript.CurrentSpeed, followResponsiveness);

        // Calculates the target position for the camera based on the car's position and the offset
        Vector3 targetPosition = car.position + car.TransformDirection(offset);

        // Smoothly moves the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    // Handles the orientation of the camera to always look at the car
    void HandleRotation()
    {
        // Calculates the direction in which the camera should look, towards the car
        Vector3 lookDirection = car.position - transform.position;
        // Creates a rotation that looks in the direction of the car
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        // Smoothly rotates the camera to face the car
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }
}