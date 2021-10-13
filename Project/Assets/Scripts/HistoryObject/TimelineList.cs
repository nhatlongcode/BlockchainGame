using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelfBalancedTree;

[Serializable]
public class TimelineList<T> where T : IEquatable<T>
{
    private static Comparer<Tuple<float, T>> comparer = Comparer<Tuple<float, T>>.Create(
            (item1, item2) => item1.Item1.CompareTo(item2.Item1));
    private AVLTree<Tuple<float, T>> timeline = new AVLTree<Tuple<float, T>>(null, 
        comparer);
    public List<Tuple<float, T>> Timeline
    {
        get => new List<Tuple<float, T>>(timeline.ValuesCollection);
        set
        {
            timeline = new AVLTree<Tuple<float, T>>(value, comparer);
        }
    }

    Tuple<float, T> minTuple = null;//new Tuple<float, T>(float.MinValue, default);

    public void Add(float time, T action)
    {
        var maxTuple = Tuple.Create(time - Constant.epsilonTime, action);
        Tuple<float, T> top = timeline.LowerBound(maxTuple);
        // Timeline prune of future events due to past events changing.
        if (top != null)
            timeline.Split(
            top,
            AVLTree<Tuple<float, T>>.SplitOperationMode.IncludeSplitValueToLeftSubtree,
            out timeline, out _);

        if (top != null && top.Item2.Equals(action))
        {
            return;
        }
        else
        {
            timeline.Add(Tuple.Create(time, action));
        }
    }

    public T GetAction(float time)
    {
        var maxTuple = Tuple.Create<float, T>(time + Constant.epsilonTime, default);
        return timeline.LowerBound(maxTuple).Item2;
    }
}
