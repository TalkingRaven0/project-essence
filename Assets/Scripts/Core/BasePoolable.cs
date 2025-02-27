using UnityEngine;
using Zenject;

public class BasePoolable : MonoBehaviour
{
    [Inject] protected ActivePoolables activePoolables;

    [SerializeField] protected float activeTime;
    [SerializeField] protected float maximumDistance;

    protected BasePooler parentPooler;
    protected Rigidbody rigidbodyComponent;

    protected Vector3 previousPosition;
    protected float activeTimer;

    protected bool hasDespawned;

    protected ISpawnData spawnData;

    // Happens during Awake() of parent pooler
    public virtual void InitializePoolable(BasePooler parent)
    {
        parentPooler = parent;
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        if (hasDespawned)
            return;

        activeTimer += Time.deltaTime;

        if(activeTimer >= activeTime)
        {
            Despawn();
            return;
        }


        if(maximumDistance != 0 && previousPosition != Vector3.zero)
        {
            var distance = Mathf.Abs((previousPosition - transform.position).magnitude);

            if (distance >= maximumDistance)
            {
                Despawn();
                return;
            }
        }

        previousPosition = transform.position;

    }

    public virtual void OnSpawn(GameObject customParent = null, ISpawnData data = null)
    {
        gameObject.transform.parent = (customParent != null ? customParent.transform : activePoolables.transform);
        spawnData = data;
        activeTimer = 0f;
        previousPosition = Vector3.zero;
        hasDespawned = false;

        gameObject.SetActive(true);
    }

    protected virtual void Despawn()
    {
        hasDespawned = true;
        parentPooler.RegisterAsPoolable(this);
        gameObject.SetActive(false);
        gameObject.transform.parent = parentPooler.transform;

        if(rigidbodyComponent != null)
        {
            rigidbodyComponent.velocity = Vector3.zero;
            rigidbodyComponent.isKinematic = true;
        }
    }
}
