using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryObject : MonoBehaviour
{
    public float time;
    
    // Update is called once per frame
    public virtual void Update()
    {
        transform.position = GetPosition(time);
    }

    public Vector2 GetPosition(float time)
    {
        return GetMovementStrategy(time).CalculateLocation(time);
    }

    private MovementStrategy GetMovementStrategy(float time)
    {
        if (history.Count == 0 || history[0].StartTime > time)
            Debug.LogError(this + "This object's movement strategy " +
                "is not defined at this time: " + time);

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
    public void AddMovementStrategy(MovementStrategy movement, float time)
    {
        if (history.Count > 0)
        {
            // Insert and recalculate events.
            MovementStrategy top = history[history.Count - 1];
            if (top.StartTime > time)
            {
                history.RemoveAt(history.Count - 1);
                AddMovementStrategy(movement, time);
                AddMovementStrategy(top, top.StartTime);
            }
            else
            {
                movement.StartPosition = top.CalculateLocation(time);
                movement.StartVelocity = top.CalculateSpeed(time);
                movement.StartTime = time;
                history.Add(movement);
            }
        }
        else
            history.Add(movement);
    }

    public List<MovementStrategy> history = new List<MovementStrategy>();
}
