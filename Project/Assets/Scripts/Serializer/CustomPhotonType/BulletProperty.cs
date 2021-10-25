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

    static BulletProperty()
    {
        Serializer.deserializeDictionary.Add(typeof(BulletProperty), Serializer.DeserializeBulletProperty);
    }
}

public partial class Serializer
{
    public static void Serialize(BulletProperty value, ref byte[] bytes)
    {
        Serialize(value.position, ref bytes);
        Serialize(value.shootTime, ref bytes);
        Serialize(value.velocity, ref bytes);
    }

    public static object DeserializeBulletProperty(byte[] bytes, ref int offset)
    {
        BulletProperty result = new BulletProperty();

        result.position = Deserialize<Vector2>(bytes, ref offset);
        result.shootTime = Deserialize<float>(bytes, ref offset);
        result.velocity = Deserialize<Vector2>(bytes, ref offset);

        return result;
    }
}
