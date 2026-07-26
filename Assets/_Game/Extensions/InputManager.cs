using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputSystem_Actions inputActions;
    public InputAction MoveAction { get; private set; }

    private void Awake()
    {
        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
            MoveAction = inputActions.Player.Move;
            inputActions.Enable();
        }
    }

    private void OnDestroy()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
            inputActions.Dispose();
        }
    }
}
