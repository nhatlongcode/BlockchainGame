using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;

[Serializable]
public class Event<T> : IEquatable<Event<T>>, IComparable<Event<T>>
    where T : IEquatable<T>
{
    public float Item1 { get; set; }
    public T Item2 { get; set; }

    public int CompareTo(Event<T> other)
    {
        return Item1.CompareTo(other.Item1);
    }

    public bool Equals(Event<T> other)
    {
        return Item1.Equals(other.Item1) && Item2.Equals(other.Item2);
    }

    public static Event<T> Create(float item1, T item2)
    {
        Event<T> result = new Event<T>();
        result.Item1 = item1;
        result.Item2 = item2;
        return result;
    }
    public static byte[] Serialize(object customObject)
    {
        Event<T> item = customObject as Event<T>;
        byte[] bytes = new byte[0];

        Serializer.Serialize(item.Item1, ref bytes);
        Serializer.Serialize(item.Item2, ref bytes, true);

        return bytes;
    }

    public static object Deserialize(byte[] bytes)
    {
        Event<T> result = new Event<T>();
        int offset = 0;

        result.Item1 = Serializer.Deserialize<float>(bytes, ref offset);
        result.Item2 = Serializer.Deserialize<T>(bytes, ref offset);

        return result;
    }
}

public class Event
{
    public static Event<T> Create<T>(float item1, T item2)
        where T : IEquatable<T>
    {
        Event<T> result = new Event<T>();
        result.Item1 = item1;
        result.Item2 = item2;
        return result;
    }
}