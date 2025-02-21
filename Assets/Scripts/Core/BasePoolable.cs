using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BasePoolable : MonoBehaviour
{
    [SerializeField] private BasePooler parentPooler;

    public virtual void InitializePoolable(BasePooler parent)
    {
        parentPooler = parent;
    }

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    protected virtual void Despawn()
    {
        parentPooler.RegisterAsPoolable(this);
        gameObject.SetActive(false);
    }
}
