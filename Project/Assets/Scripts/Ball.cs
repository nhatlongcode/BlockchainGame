using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : HistoryObject
{
    public float initialTime;

    // Start is called before the first frame update
    void Start()
    {
        MovementStraight straight = new MovementStraight();
        straight.StartPosition = transform.position;
        straight.StartVelocity = new Vector2(0, 1);
        straight.StartTime = 0;

        MovementParabola parabola = new MovementParabola();

        MovementStraight straight2 = new MovementStraight();

        MovementParabola parabola2 = new MovementParabola(1);

        MovementParabola parabola3 = new MovementParabola();

        MovementStraight straight3 = new MovementStraight();

        AddMovementStrategy(straight, 0);
        AddMovementStrategy(straight3, 11);
        AddMovementStrategy(parabola3, 10);
        AddMovementStrategy(parabola2, 8);
        AddMovementStrategy(straight2, 5);
        AddMovementStrategy(parabola, 3);

        initialTime = Time.time;
        time = 0;
    }
    public bool paused;
    public override void Update()
    {
        if (!paused)
            time += Time.deltaTime;
        base.Update();
    }
}
