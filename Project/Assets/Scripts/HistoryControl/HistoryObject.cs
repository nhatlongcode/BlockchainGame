using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryObject : MonoBehaviour
{
    public float currentTime;
    public HistoryObject spawner;
    public float spawnTime;
    public bool isPrefab = true;

    protected virtual void Start()
    {
        spawnTime = currentTime;
        if (!isPrefab)
            History.Inst.historyObjectList.Add(this);
    }

    // Update is called by History once per frame
    public virtual void MyUpdate()
    {
        Vector2? pos = GetPosition(currentTime);
        if (pos != null)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            transform.position = pos.Value;
        }
        else
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }

    public Vector2? GetPosition(float time)
    {
        return GetMovementStrategy(time)?.CalculateLocation(time);
    }

    private ActionStrategy GetMovementStrategy(float time)
    {
        if (history.Count == 0 || history[0].StartTime > time)
            return null;

        int l = 1;
        int r = history.Count - 1;
        while (l <= r)
        {
            int m = (l + r) / 2;
            if (history[m].StartTime < time)
                l = m + 1;
            else
                r = m - 1;
        }
        return history[r];
    }

    /// <summary>
    /// Add Movement Strategy, with continuous position and velocity.
    /// If the movement strategy is the first, please specify its initial position 
    /// and initial velocity.
    /// </summary>
    /// <param name="movement"></param>
    /// <param name="time"></param>
    /// 
    public void AddActionStrategy(ActionStrategy action, float time)
    {
        _AddActionStrategy(action, time);
        action.OnAdd(this);
    }

    private void _AddActionStrategy(ActionStrategy movement, float time)
    {
        if (history.Count > 0)
        {
            // Insert and recalculate events.
            ActionStrategy top = history[history.Count - 1];
            if (top.StartTime > time)
            {
                history.RemoveAt(history.Count - 1);
                _AddActionStrategy(movement, time);
                _AddActionStrategy(top, top.StartTime);
            }
            else
            {
                try
                {
                    movement.StartPosition = top.CalculateLocation(time).Value;
                    movement.StartVelocity = top.CalculateSpeed(time).Value;
                    movement.StartTime = time;
                    history.Add(movement);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
        else
            history.Add(movement);
    }

    public virtual void OnDelete()
    {
        for (int i = history.Count - 1; i >= 0; i--)
        {
            history[i].OnDelete(this);
        }
        gameObject.SetActive(false);
    }

    public virtual void Revert(float time)
    {
        int lastActionIndex = history.Count - 1;
        while (lastActionIndex >= 0 && history[lastActionIndex].StartTime > time)
        {
            lastActionIndex--;
        }
        lastActionIndex++;
        if (lastActionIndex < history.Count)
            history.RemoveRange(lastActionIndex, history.Count - lastActionIndex);
    }

    public virtual List<HistoryObject> SpawnedObjects(float time)
    {
        List<HistoryObject> result = new List<HistoryObject>();
        foreach (ActionStrategy action in history)
        {
            HistoryObject obj = action.SpawnedObject(time);
            if (obj != null)
                result.Add(obj);
        }
        return result;
    }

    public List<ActionStrategy> history = new List<ActionStrategy>();
}
