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
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(_carGameObject);
        }
    }
    
}
