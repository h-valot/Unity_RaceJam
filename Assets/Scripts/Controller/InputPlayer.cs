using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    
    private ControlMode _control;

    private enum ControlMode
    {
        KEYBOARD,
        BUTTONS
    }

    private void Update()
    { 
        switch(_control)
        {
            case ControlMode.KEYBOARD:
                verticalInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");
                break;
            case ControlMode.BUTTONS:
                verticalInput = Input.GetAxis("Joystick Vertical");
                horizontalInput = Input.GetAxis("Joystick Horizontal");
                break;
        }
    }

    /// <summary>
    /// Returns TRUE if input axis aren't equals to zero 
    /// </summary>
    public bool DoGatherInput()
    {
        return horizontalInput != 0 || verticalInput != 0;
    }
}
