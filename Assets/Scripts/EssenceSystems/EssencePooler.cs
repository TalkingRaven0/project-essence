using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EssencePooler : BasePooler
{
    // variable of UniRX event
    public Subject<EssenceType> OnEssenceSpawned;

    public void SpawnPoolable(Vector3 position, EssenceType type)
    {
        if (poolables.Count < 1)
        {
            Debug.Log("Not enough poolables");
            return;
        }

        EssencePoolable spawnedPoolable = poolables[0] as EssencePoolable;

        if (spawnedPoolable == null)
            return;

        spawnedPoolable.transform.position = position;
        spawnedPoolable.OnSpawn(type);

        // Send Out Event for UniRX
        OnEssenceSpawned.OnNext(type);

        poolables.Remove(spawnedPoolable);
    }

}
