//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class CameraController : MonoBehaviour
//{
//
//    [SerializeField] private Transform car; // Reference to the car's Transform
//    [SerializeField] private MovementCar carMovementScript; // Reference to the car's movement script
//    [SerializeField] private Vector3 offset; // Offset of the camera from the car
//    [SerializeField] private float lookSpeed = 5f; // Speed at which the camera orients towards the car
//    private float followResponsiveness = 0.3f;
//
//
//    void LateUpdate()
//    {
//        HandleTranslation();
//        HandleRotation();
//    }
//
//    // Handles the position of the camera based on the car's position
//    void HandleTranslation()
//    {
//        // Gets the current speed of the car from the MovementCar script
//        float followSpeed = Mathf.Lerp(transform.position.magnitude, carMovementScript.CurrentSpeed, followResponsiveness);
//
//        // Calculates the target position for the camera based on the car's position and the offset
//        Vector3 targetPosition = car.position + car.TransformDirection(offset);
//
//        // Smoothly moves the camera to the target position
//        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
//    }
//
//    // Handles the orientation of the camera to always look at the car
//    void HandleRotation()
//    {
//        // Calculates the direction in which the camera should look, towards the car
//        Vector3 lookDirection = car.position - transform.position;
//        // Creates a rotation that looks in the direction of the car
//        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
//        // Smoothly rotates the camera to face the car
//        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
//    }
//}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform carTransform; // La référence au transform de la voiture
    //public Vector3 offset; // Le décalage initial de la caméra par rapport à la voiture
    //public float followSpeed = 10f; // Vitesse à laquelle la caméra suit la voiture
    //public float lookAtSpeed = 10f; // Vitesse de rotation de la caméra pour suivre la direction de la voiture
    //public LayerMask obstaclesLayer; // Les layers considérés comme des obstacles
//
    //private Vector3 currentOffset;
    //public float minimumDistanceToObstacle = 1f; // Distance minimale entre la caméra et un obstacle
//
    //private Vector3 desiredPosition;
    //private Vector3 smoothedPosition;
    //private void Start()
    //{
    //    // Initialise l'offset actuel avec l'offset initial
    //    currentOffset = offset;
    //}
//
    //private void LateUpdate()
    //{
    //    // Met à jour la position de la caméra
    //    HandleTranslation();
    //    // Met à jour l'orientation de la caméra
    //    HandleRotation();
    //    // Ajuste l'offset si un obstacle est détecté
    //    //AdjustForObstacles();
    //}
//
    //private void HandleTranslation()
    //{
    //    // Position cible de la caméra
    //    Vector3 targetPosition = carTransform.position + carTransform.TransformDirection(currentOffset);
    //    // Mouvement lisse de la caméra vers la position cible
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    //}
//
    //private void HandleRotation()
    //{
    //    // Direction vers laquelle la caméra doit regarder
    //    Vector3 lookDirection = carTransform.position - transform.position;
    //    // Rotation lisse de la caméra pour toujours regarder la voiture
    //    Quaternion rotation = Quaternion.LookRotation(lookDirection);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookAtSpeed * Time.deltaTime);
    //}
//
    //private void AdjustForObstacles()
    //{
    //    // Calculer la position désirée basée sur l'offset
    //    desiredPosition = carTransform.position + carTransform.TransformDirection(offset);
    //    
    //    // Vérifier les obstacles entre la caméra et la position désirée
    //    RaycastHit hit;
    //    if (Physics.Raycast(carTransform.position, desiredPosition - carTransform.position, out hit, offset.magnitude, obstaclesLayer))
    //    {
    //        // Si un obstacle est détecté, ajuste la position désirée pour être juste devant l'obstacle, en respectant la distance minimale
    //        desiredPosition = hit.point - (hit.normal * minimumDistanceToObstacle);
    //    }
//
    //    // Interpoler doucement vers la position désirée
    //    smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
//
    //    // Ajuster la position de la caméra et la faire regarder vers la voiture
    //    transform.position = smoothedPosition;
    //    transform.LookAt(carTransform.position, Vector3.up);
    //    
    //}
    
    
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform carTarget;

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}