using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSmoothness;
    [SerializeField] private float rotSmoothness;

    [SerializeField] private Vector3 forwardMoveOffset;
    [SerializeField] private Vector3 backwardMoveOffset;
    [SerializeField] private Vector3 rotOffset;

    [SerializeField] private Transform carTarget;

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 targetPos;
        if (carTarget.InverseTransformDirection(carTarget.GetComponent<Rigidbody>().velocity).z < 0)
        {
            targetPos = carTarget.TransformPoint(backwardMoveOffset);
        }
        else
        {
            targetPos = carTarget.TransformPoint(forwardMoveOffset);
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}

