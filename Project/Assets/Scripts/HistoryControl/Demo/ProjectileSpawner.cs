using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : HistoryObject
{
    public HistoryObject prefab;
    protected override void Start()
    {
        base.Start();

    }
    int count = 0;
    public override void MyUpdate()
    {
        if (count * 4 < time)
        {
            prefab.time = count * 4 + 1;
            HistoryObject go = Instantiate(prefab, transform.position, transform.rotation);
            ActionSpawnObject action = new ActionSpawnObject(go, 1);
            action.StartPosition = transform.position;
            action.StartVelocity = new Vector2(0, 0);

            AddActionStrategy(action, count * 4);
            count++;
        }
        base.MyUpdate();
    }
}
