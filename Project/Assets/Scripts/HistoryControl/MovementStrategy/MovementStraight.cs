using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStraight : MovementStrategy
{
    public override Vector2 CalculateLocation(float time)
    {
        return StartVelocity * (time - this.StartTime) + StartPosition;
    }

    public override Vector2 CalculateSpeed(float time)
    {
        return StartVelocity;
    }
}
