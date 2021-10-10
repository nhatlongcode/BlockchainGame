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
        parabol.StartTime = currentTime;

        AddActionStrategy(parabol, currentTime);
    }
    bool despawned = false;
    public override void MyUpdate()
    {
        base.MyUpdate();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.activeSelf && armed == float.PositiveInfinity)
            armed = currentTime;
    }

    float armed = float.PositiveInfinity;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (armed > currentTime)
        {
            return;
        }
        if (!despawned)
        {
            AddActionStrategy(new ActionDespawn(), currentTime);
            despawned = true;
        }
        else
            Debug.LogWarning("despawn twice");
    }
}
