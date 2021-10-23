using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : IEquatable<BulletProperty>, IComparable<BulletProperty>
{
    [SerializeField]
    public Vector2 position;
    [SerializeField]
    public Vector2 velocity;
    [SerializeField]
    public float shootTime;

    public int CompareTo(BulletProperty other)
    {
        return shootTime.CompareTo(other.shootTime);
    }

    public bool Equals(BulletProperty other)
    {
        return shootTime == other.shootTime && position == other.position && velocity == other.velocity;
    }
}
