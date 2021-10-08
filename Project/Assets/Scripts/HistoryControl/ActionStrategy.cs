using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionStrategy
{
    public abstract Vector2? CalculateLocation(float time);

    public abstract Vector2? CalculateSpeed(float time);

    public virtual HistoryObject SpawnedObject(float time) { return null; }

    public virtual void OnDelete(HistoryObject _) { }
    public virtual void OnAdd(HistoryObject _) { }
    
    public Vector2 StartPosition;
    public Vector2 StartVelocity;
    public float StartTime;
}
