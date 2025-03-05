using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxResponse
{
    public Hurtbox hurtbox;
    public IHittable parent;

    public HurtboxResponse(Hurtbox hurtbox, IHittable parent)
    {
        this.hurtbox = hurtbox;
        this.parent = parent;
    }

    public bool CheckHitValidity() { return true; }
}

public interface IHittable
{
    public virtual HurtboxResponse RegisterHit(HitData data) { return null; }
}

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private IHittable parent;

    public virtual HurtboxResponse GetHurtboxResponse(HitData hitboxData)
    {
        // Tell Parent that it has been hit
        // Parent can return a response
       HurtboxResponse response = parent.RegisterHit(hitboxData);

        // Create generic response if response is not made by parent
        if(response == null)
            response = new HurtboxResponse(this, parent);

        return response;
    }
}
