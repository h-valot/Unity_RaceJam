using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    private ControlMode control;
    
    private enum ControlMode
    {
        Keyboard,
        Buttons
    };

    void Update()
    { 
        switch(control)
        {
            case ControlMode.Keyboard:
                verticalInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");
                break;
            case ControlMode.Buttons:
                verticalInput = Input.GetAxis("Joystick Vertical");
                horizontalInput = Input.GetAxis("Joystick Horizontal");
                break;
        }
    }
}
