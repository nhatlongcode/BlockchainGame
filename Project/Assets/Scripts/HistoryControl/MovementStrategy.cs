using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategy
{
    public abstract Vector2 CalculateLocation(float time);

    public abstract Vector2 CalculateSpeed(float time);

    public Vector2 StartPosition;
    public Vector2 StartVelocity;
    public float StartTime;
}
