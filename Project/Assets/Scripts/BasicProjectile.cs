using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTerrain;

public class BasicProjectile : HistoryObject, IEquatable<BasicProjectile>
{
    Dictionary<int, bool> active = new Dictionary<int, bool>();
    public Collider2D collider;
    [NonSerialized]
    public float TimeToArm;
    public const float speed = 3;

    public const int destroyCircleSize = 8;
    Shape destroyCircle;

    BasicProjectile()
    {
        TimeToArm = (History.Inst?.time??0)
            + Constant.epsilonTimeProjectileExit;
    }

    public bool Equals(BasicProjectile other)
    {
        return this == other;
    }

    protected override void Start()
    {
        base.Start();

        destroyCircle = Shape.GenerateShapeCircle(destroyCircleSize);

        if (IsTimeStopped)
            tempVelocity = transform.up * speed;
        else
            rigidbody.velocity = transform.up * speed;

        if (collider == null)
            collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public override void MyUpdate()
    {
        base.MyUpdate();

        bool colliderShouldEnable = History.Inst.time > TimeToArm;
        if (collider.enabled != colliderShouldEnable)
        {
            collider.enabled = colliderShouldEnable;
        }
    }

    public override void Save(int id)
    {
        base.Save(id);

        active[id] = gameObject.activeSelf;
    }

    public override void Restore(int id)
    {
        base.Restore(id);

        if (active.ContainsKey(id))
            gameObject.SetActive(active[id]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Terrain.Inst.DestroyTerrain(collision.GetContact(0).point, destroyCircle);
        gameObject.SetActive(false);
    }
}
