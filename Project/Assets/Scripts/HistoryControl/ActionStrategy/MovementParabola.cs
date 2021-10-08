using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParabola : ActionStrategy
{
    public const float gravity = -1;

    public float X_Velocity { get => StartVelocity.x; }
    public float Y_Velocity { get => StartVelocity.y; }
    public float Y_Acceleration;

    public MovementParabola(float gravity = gravity)
    {
        Y_Acceleration = gravity;
    }

    public override Vector2? CalculateLocation(float time)
    {
        float deltaTime = time - this.StartTime;

        float X_Pos = StartPosition.x + X_Velocity * deltaTime;
        float Y_Pos = StartPosition.y + Y_Velocity * deltaTime 
            + Y_Acceleration * deltaTime * deltaTime / 2;
        return new Vector2(X_Pos, Y_Pos);
    }

    public override Vector2? CalculateSpeed(float time)
    {
        float X_Vel = X_Velocity;
        float Y_Vel = Y_Velocity + Y_Acceleration * (time - this.StartTime);

        return new Vector2(X_Vel, Y_Vel);
    }
}
