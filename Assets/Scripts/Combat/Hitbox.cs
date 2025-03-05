using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class HitData
{
    public Hitbox source;
    public Hurtbox target;

    public HitData(Hitbox source, Hurtbox target = null)
    {
        this.source = source;
        this.target = target;
    }
}

public interface IHitter
{
    public void HasHitHurtbox(HurtboxResponse response, HitData hitData) { }
}

public class Hitbox : MonoBehaviour
{
    [SerializeField] private IHitter parent;

    private List<Hurtbox> hurtboxHitList = new();

    private void OnEnable()
    {
        // Reset Hit List every enable/disable
        hurtboxHitList.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox hurtbox = collision.GetComponent<Hurtbox>();

        // Handling Of Non-Hurtbox Collision
        if (hurtbox == null)
        {
            Debug.LogWarning("Hitbox hit something thats not a Hurtbox!", this);
            Debug.LogWarning("Check entity physics layer", collision.gameObject);
            return;
        }

        // Prevent Multiple Hits
        if (hurtboxHitList.Contains(hurtbox))
            return;

        HandleHit(hurtbox);
    }

    // Override this function if there is a different version of HitData
    protected virtual void HandleHit(Hurtbox hurtbox)
    {
        HitData hitData = new HitData(this, hurtbox);
        HurtboxResponse response = hurtbox.GetHurtboxResponse(hitData);
        hitData.target = hurtbox;

        // Notify parent that it has hit something
        parent.HasHitHurtbox(response, hitData);

        // Register to List by default if there is no response
        if (response == null || response.CheckHitValidity())
            hurtboxHitList.Add(hurtbox);
    }

}
}
