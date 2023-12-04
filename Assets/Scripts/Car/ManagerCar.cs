using System.Data.SqlTypes;
using UnityEngine;

public class ManagerCar : MonoBehaviour
{
    private InputPlayer inputPlayer;
    private MovementCar movementCar;
    [SerializeField] private GameObject _carGameObject;

    void Start()
    {
        inputPlayer = GetComponent<InputPlayer>();
        movementCar = GetComponent<MovementCar>();
    }

    void Update()
    {
        movementCar.ReceiveInput(inputPlayer.horizontalInput, inputPlayer.verticalInput);
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        // exit, if the collided object isn't a wall
        if (!collision.gameObject.CompareTag("Wall")) return;

        Events.onCircuitEnded?.Invoke(true);
    }
    
}
