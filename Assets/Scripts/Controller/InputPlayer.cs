using UnityEngine;
using UnityEngine.Events;

public class InputPlayer : MonoBehaviour
{
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    
    private ControlMode _control;

    public UnityEvent PressedEscapeKey;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedEscapeKey.Invoke();
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
