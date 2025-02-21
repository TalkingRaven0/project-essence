using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BasePooler : MonoBehaviour
{
    [SerializeField] protected List<BasePoolable> poolables = new();

    private void OnValidate()
    {
        var childPoolables = GetComponentsInChildren<BasePoolable>();

        poolables.Clear();

        foreach (var item in childPoolables)
        {
            item.InitializePoolable(this);
            RegisterAsPoolable(item);
        }
    }

    public virtual void RegisterAsPoolable(BasePoolable poolable)
    {
        poolables.Add(poolable);
    }

    public virtual void SpawnPoolable(Vector3 position)
    {
        if(poolables.Count < 1) 
        {
            Debug.Log("Not enough poolables");
            return;
        }

        BasePoolable spawnedPoolable = poolables[0];

        spawnedPoolable.transform.position = position;
        spawnedPoolable.OnSpawn();

        poolables.Remove(spawnedPoolable);
    }
}
