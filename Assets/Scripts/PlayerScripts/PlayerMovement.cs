using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class PlayerMovement : MonoBehaviour
{
    [Inject] protected GameObject playerObject;
    [Inject] protected InputHandler inputHandler;
    [Inject] protected Camera mainCamera;

    [SerializeField] public float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private Rigidbody playerRB;

    private Vector3 assignedVelocity;
    public Vector3 AssignedVelocity => assignedVelocity;

    private Vector3 appliedAcceleration;

    private void Start()
    {
        playerRB = playerObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleAccelerationMovement();

        assignedVelocity = appliedAcceleration;

        playerRB.velocity = assignedVelocity;
    }

    private void HandleAccelerationMovement()
    {
        Vector3 inputDirection = inputHandler.GetMoveInputRaw();
        Vector3 moveDirection = Vector3.zero;

        // Orient inputs based on camera facing
        moveDirection += inputDirection.x * mainCamera.transform.right;
        moveDirection += inputDirection.y * mainCamera.transform.forward;

        // Remove Y movement
        moveDirection.y = 0;

        // Normalize and apply movement
        moveDirection = moveDirection.normalized;

        if(moveDirection == Vector3.zero)
        {
            appliedAcceleration -= deceleration * appliedAcceleration.normalized;
        } else
        {
            appliedAcceleration += moveDirection * acceleration;
        }
        appliedAcceleration = Vector3.ClampMagnitude(appliedAcceleration, moveSpeed);
    }
}
