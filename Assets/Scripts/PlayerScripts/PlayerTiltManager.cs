using UnityEngine;
using Zenject;

public class PlayerTiltManager : MonoBehaviour
{
    [Inject] private PlayerMovement movementScript;

    [SerializeField] private float moveTiltAmount;
    [SerializeField] private Vector3 baseOffset;


    private Vector3 movementTilt;

    private Vector3 appliedTilt;

    private void Update()
    {
        appliedTilt = baseOffset;
        appliedTilt += movementTilt;

        transform.rotation = Quaternion.Euler(appliedTilt);
    }

    private void FixedUpdate()
    {
        HandleMovementTilt();
    }

    private void HandleMovementTilt()
    {
        float tiltModifier = movementScript.AssignedVelocity.magnitude / movementScript.moveSpeed;

        Vector3 scaledTiltAmount = Vector3.Lerp(Vector3.zero, movementScript.AssignedVelocity.normalized * moveTiltAmount, tiltModifier);

        movementTilt.x = scaledTiltAmount.x;
        movementTilt.z = scaledTiltAmount.z;
    }

}
