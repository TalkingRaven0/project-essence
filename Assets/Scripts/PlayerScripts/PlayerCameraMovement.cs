using UnityEngine;
using Zenject;

public class PlayerCameraMovement : MonoBehaviour
{
    [Inject] protected Camera mainCamera;
    [Inject] protected GameObject playerObject;
    [Inject] protected InputHandler inputHandler;

    [SerializeField] protected Vector3 cameraOffset;
    [SerializeField] protected float cameraLerp;
    [SerializeField] protected float distanceFactor;
    [SerializeField] protected Vector2 minMaxDistanceFactor;
    [SerializeField] protected LayerMask mouseLayerMask;
    [SerializeField] public bool AdjustOnDistance;

    private Vector3 mousePosition;
    private Vector3 mouseDirection;
    private float mouseDistance;
    private float usedDistanceFactor;

    private Vector3 cameraDestination;

    private void FixedUpdate()
    {
        mousePosition = GetMousePosition();
        mousePosition.y = playerObject.transform.position.y;

        mouseDirection = playerObject.transform.position - mousePosition;
        mouseDirection.y = playerObject.transform.position.y;

        mouseDistance = mouseDirection.magnitude;
        mouseDirection = mouseDirection.normalized;

        cameraDestination = playerObject.transform.position + cameraOffset;

        usedDistanceFactor = (mouseDirection - new Vector3(-0.5f, 0, -0.5f)).magnitude * distanceFactor;

        usedDistanceFactor = Mathf.Clamp(usedDistanceFactor, minMaxDistanceFactor.x, minMaxDistanceFactor.y);

        if(IsMouseOverGameWindow() && AdjustOnDistance)
            cameraDestination += -mouseDirection * mouseDistance * usedDistanceFactor;

        cameraDestination.y = cameraOffset.y;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraDestination, cameraLerp);
    }

    protected virtual Vector3 GetMousePosition()
    {
        RaycastHit hitInfo;
            
        if(!Physics.Raycast(mainCamera.ScreenPointToRay(inputHandler.GetScreenMousePosition()), out hitInfo, 10000f, mouseLayerMask))
            return Vector3.zero;

        return hitInfo.point;
    }

    private bool IsMouseOverGameWindow()
    {
        Vector3 mp = inputHandler.GetScreenMousePosition();
        return !(0 > mp.x || 0 > mp.y || Screen.width < mp.x || Screen.height < mp.y);
    }

}
