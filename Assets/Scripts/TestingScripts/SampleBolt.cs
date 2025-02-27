using UnityEngine;
using Zenject;

public class SampleBolt : BasePoolable
{
    [Inject] private InputHandler inputHandler;

    [SerializeField] protected float launchSpeed;
    public override void OnSpawn(GameObject customParent = null, ISpawnData data = null)
    {
        base.OnSpawn(customParent, data);

        rigidbodyComponent.isKinematic = false;
        rigidbodyComponent.useGravity = false;

        transform.LookAt((data as PowerData).clickedPoint,Vector3.up);

        rigidbodyComponent.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    protected override void Update()
    {
        base.Update();


    }
}
