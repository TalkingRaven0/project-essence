using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset InGameInputs;

    private InputAction moveInput;
    private InputAction cursorInput;
    private InputAction mouseLeft;

    private float mouseLeftHeldDuration;
    private bool mouseLeftHeld;

    private void Start()
    {
        moveInput = InGameInputs.FindAction("Move");
        moveInput.Enable();

        cursorInput = InGameInputs.FindAction("Cursor");
        cursorInput.Enable();

        mouseLeft = InGameInputs.FindAction("MouseLeft");
        mouseLeft.Enable();
    }

    private void Update()
    {
        if (GetMouseLeftClicked())
        {
            mouseLeftHeld = true;
            mouseLeftHeldDuration = 0;
        }

        if (GetMouseLeftReleased())
        {
            mouseLeftHeld = false;
        }

        if(mouseLeftHeld)
            mouseLeftHeldDuration += Time.deltaTime;
    }

    public Vector2 GetScreenMousePosition()
    {
        return cursorInput.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInputRaw()
    {
        return moveInput.ReadValue<Vector2>();
    }

    public bool GetMouseLeftClicked()
    {
        return mouseLeft.WasPressedThisFrame();
    }
    public bool GetMouseLeftReleased()
    {
        return mouseLeft.WasReleasedThisFrame();
    }
    public float GetMouseLeftHeld()
    {
        return mouseLeftHeldDuration;
    }
}
