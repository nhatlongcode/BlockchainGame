using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : HistoryObject
{
    protected override void Start()
    {
        base.Start();

        MovementParabola parabol = new MovementParabola(-4);
        parabol.StartVelocity = new Vector2(2, 4);
        parabol.StartPosition = spawner.transform.position;
        parabol.StartTime = time;

        AddActionStrategy(parabol, time);
    }
    bool despawned = false;
    public override void MyUpdate()
    {
        base.MyUpdate();

        if (transform.position.y < -2 && !despawned)
        {
            AddActionStrategy(new ActionDespawn(), time);
            despawned = true;
        }
    }
}
