using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    [Range(0.001f, 5f)] public float radius = 0.1f;
    private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, radius);
}
