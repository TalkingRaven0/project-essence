using System.Collections.Generic;
using UnityEngine;

public class BasePooler : MonoBehaviour
{
    /// <summary>
    /// Populate List on validate but initialize parent on Awake()
    /// </summary>

    [SerializeField] protected List<BasePoolable> poolables = new();

    private void OnValidate()
    {
        var childPoolables = GetComponentsInChildren<BasePoolable>();

        poolables.Clear();

        foreach (var item in childPoolables)
        {
            RegisterAsPoolable(item);
        }
    }

    private void Awake()
    {
        foreach (var item in poolables)
        {
            item.InitializePoolable(this);
            item.gameObject.SetActive(false);
        }
    }

    public virtual void RegisterAsPoolable(BasePoolable poolable)
    {
        poolables.Add(poolable);
    }

    public virtual void SpawnPoolable(Vector3 position, GameObject customParent = null, ISpawnData data = null)
    {
        if(poolables.Count < 1) 
        {
            Debug.Log("Not enough poolables");
            return;
        }

        BasePoolable spawnedPoolable = poolables[0];

        spawnedPoolable.transform.position = position;
        spawnedPoolable.OnSpawn(customParent, data);

        poolables.Remove(spawnedPoolable);
    }
}

public interface ISpawnData { }
