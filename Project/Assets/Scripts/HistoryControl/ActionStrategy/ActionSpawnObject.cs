using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSpawnObject : ActionStrategy
{
    public HistoryObject objectSpawned;
    // Obj assumed to be instantiated.
    public ActionSpawnObject(HistoryObject obj, float timeToSpawn)
    {
        objectSpawned = obj;
        obj.spawnTime = timeToSpawn;
    }

    public override Vector2? CalculateLocation(float time)
    {
        return StartPosition;
    }

    public override Vector2? CalculateSpeed(float time)
    {
        return new Vector2(0, 0);
    }

    public override HistoryObject SpawnedObject(float time)
    {
        if (time > objectSpawned.spawnTime)
            return objectSpawned;
        else
            return null;
    }

    public override void OnAdd(HistoryObject obj)
    {
        History.Inst.historyObjectList.Add(objectSpawned);
        objectSpawned.spawner = obj;
    }

    public override void OnDelete(HistoryObject _)
    {
        History.Inst.historyObjectList.Remove(objectSpawned);
        objectSpawned.OnDelete();
    }
}
