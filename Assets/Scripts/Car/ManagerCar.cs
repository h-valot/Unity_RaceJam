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
        movementCar.ReceiveInput(inputPlayer.horizontalInput, inputPlayer.verticalInput);
    }
}
