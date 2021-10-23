using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[Serializable]
public class StackList<T> : List<T>
{
    public T Peek() { return this[Count - 1]; }
    public void Pop()
    {
        RemoveAt(Count - 1);
    }
    public void Push(T item)
    {
        Add(item);
    }
}

[Serializable]
public class TimelineList<T> where T : IEquatable<T>
{
    public StackList<Tuple<float, T>> Timeline = new StackList<Tuple<float, T>>();

    public float Top()
    {
        if (Timeline.Count > 0)
            return Timeline.Peek().Item1;
        else
            return 0;
    }

    public void Add(float time, T action)
    {
        while (Timeline.Count > 0 && Timeline.Peek().Item1 > time)
            Timeline.Pop();

        if (Timeline.Count > 0 && Timeline.Peek().Item2.Equals(action))
        {
            return;
        }
        else
        {
            Timeline.Push(Tuple.Create(time, action));
        }
    }

    public T GetAction(float time)
    {
        int r = binSearch(time);
        if (r >= 0)
            return Timeline[r].Item2;
        else
            return default;
    }

    // Remove all events before time
    public void PruneFront(float time)
    {
        int r = binSearch(time);
        if (r >= 0)
            Timeline.RemoveRange(0, r + 1);
    }

    public List<Tuple<float, T>> AllEventsAfterTime(float time)
    {
        return new List<Tuple<float, T>>(Timeline.Where(item => item.Item1 > time));
    }

    private int binSearch(float time)
    {
        int l = 0;
        int r = Timeline.Count - 1;
        while (l <= r)
        {
            int m = (l + r) / 2;
            if (Timeline[m].Item1 < time)
                l = m + 1;
            else
                r = m - 1;
        }
        return r;
    }
}
