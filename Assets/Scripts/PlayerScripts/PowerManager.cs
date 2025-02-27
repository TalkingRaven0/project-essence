using UnityEngine;
using Zenject;
using UniRx;

public class PowerData : ISpawnData
{
    public PowerData(Vector3 clickedPoint)
    {
        this.clickedPoint = clickedPoint;
    }

    public Vector3 clickedPoint;
}

public class PowerManager : MonoBehaviour
{
    [Inject] private InputHandler inputHandler;
    [Inject] private GameObject playerObject;
    [Inject] private Camera mainCamera;

    [SerializeField] private BasePooler boltPool;
    [SerializeField] private LayerMask clickMask;

    private BasePooler activePool;

    private void Start()
    {
        inputHandler.MouseClicked.Subscribe(_ =>
        {
            if (activePool != null)
            {
                activePool.SpawnPoolable(activePool.transform.position, null, new PowerData(GetClickedPoint()));
                Debug.Log("fired active power");
                return;
            }

            Debug.Log("No active power");
        });

        inputHandler.Power1.Subscribe(_ => {
            activePool = boltPool;
            Debug.Log("Activated Magic Bolt");
        });
    }
    protected virtual Vector3 GetClickedPoint()
    {
        RaycastHit hitInfo;

        if (!Physics.Raycast(mainCamera.ScreenPointToRay(inputHandler.GetScreenMousePosition()), out hitInfo, 10000f, clickMask))
            return Vector3.zero;

        Vector3 hitpoint = hitInfo.point;

        hitpoint.y = activePool.transform.position.y;

        return hitpoint;
    }
}
