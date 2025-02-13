using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset InGameInputs;

    private InputAction moveInput;
    private InputAction cursorInput;

    private void Start()
    {
        moveInput = InGameInputs.FindAction("Move");
        cursorInput = InGameInputs.FindAction("Cursor");

        moveInput.Enable();
        cursorInput.Enable();
    }

    public Vector2 GetScreenMousePosition()
    {
        return cursorInput.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInputRaw()
    {
        return moveInput.ReadValue<Vector2>();
    }
}
