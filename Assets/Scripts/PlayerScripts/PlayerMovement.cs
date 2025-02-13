using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class PlayerMovement : MonoBehaviour
{
    [Inject] protected GameObject playerObject;
    [Inject] protected InputHandler inputHandler;
    [Inject] protected Camera mainCamera;

    [SerializeField] private float moveSpeed;

    private Rigidbody playerRB;

    private Vector3 assignedVelocity;

    private void Start()
    {
        playerRB = playerObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleBasicMovement();

        playerRB.velocity = assignedVelocity;
    }

    private void HandleBasicMovement()
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
        assignedVelocity = moveDirection * moveSpeed;

    }
}
