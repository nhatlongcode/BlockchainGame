using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Despawns. Every future action is deleted
public class ActionDespawn : ActionStrategy
{
    public override Vector2? CalculateLocation(float time)
    {
        return null;
    }

    public override Vector2? CalculateSpeed(float time)
    {
        return null;
    }

    public override void OnAdd(HistoryObject obj)
    {
        List<ActionStrategy> history = obj.history;
        int count = history.Count;
        int i = count - 1;

        while (i >= 0 && history[i] != this) 
        {
            i--;
        }

        if (i<0)
        {
            Debug.LogError("Action is not actually added.");
        }
        else
        {
            i++;
            // Permanently destroy every event after despawning.
            for (int j = i; j < count; j++)
                history[j].OnDelete(obj);
            if (count > i)
                history.RemoveRange(i, count - i);
        }
    }
}
