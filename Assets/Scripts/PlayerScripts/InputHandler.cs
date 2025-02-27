using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset InGameInputs;

    private InputAction moveInput;
    private InputAction cursorInput;
    private InputAction mouseLeft;
    private InputAction pow1;
    private InputAction pow2;
    private InputAction pow3;
    private InputAction pow4;

    private float mouseLeftHeldDuration;
    private bool mouseLeftHeld;

    public IObservable<long> MouseClicked;
    public IObservable<long> MouseReleased;
    public IObservable<long> Power1;
    public IObservable<long> Power2;
    public IObservable<long> Power3;
    public IObservable<long> Power4;
    public Subject<float> MouseHeld;

    private void Awake()
    {
        SetupInput(ref moveInput, "Move");
        SetupInput(ref cursorInput, "Cursor");
        SetupInput(ref mouseLeft, "MouseLeft");
        SetupInput(ref pow1, "Power1");
        SetupInput(ref pow2, "Power2");
        SetupInput(ref pow3, "Power3");
        SetupInput(ref pow4, "Power4");

        Observables();
        Subscriptions();
    }
    
    private void Observables()
    {
        // Bindings for both mouse press and release
        MouseClicked = Observable.EveryUpdate()
            .Where(_ => mouseLeft.WasPressedThisFrame());

        MouseReleased = Observable.EveryUpdate()
            .Where(_ => mouseLeft.WasReleasedThisFrame());

        // Powers bound to button release
        Power1 = Observable.EveryUpdate()
            .Where(_ => pow1.WasReleasedThisFrame());

        Power2 = Observable.EveryUpdate()
            .Where(_ => pow2.WasReleasedThisFrame());

        Power3 = Observable.EveryUpdate()
            .Where(_ => pow3.WasReleasedThisFrame());

        Power4 = Observable.EveryUpdate()
            .Where(_ => pow4.WasReleasedThisFrame());
    }

    private void Subscriptions()
    {
        // For tracking mouse hold
        MouseClicked.Subscribe(_ =>
        {
            mouseLeftHeld = true;
        });

        MouseReleased.Subscribe(_ =>
        {
            mouseLeftHeld = false;
            mouseLeftHeldDuration = 0f;
        });
    }

    private void Update()
    {
        if (mouseLeftHeld)
        {
            mouseLeftHeldDuration += Time.deltaTime;
        }
    }

    private void SetupInput(ref InputAction actionRef ,string action)
    {
        actionRef = InGameInputs.FindAction(action);
        actionRef.Enable();
    }

    public Vector2 GetScreenMousePosition()
    {
        return cursorInput.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInputRaw()
    {
        return moveInput.ReadValue<Vector2>();
    }

    public float GetMouseLeftHeld()
    {
        return mouseLeftHeldDuration;
    }
}
