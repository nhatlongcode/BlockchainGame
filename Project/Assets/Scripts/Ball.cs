using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : HistoryObject
{
    //public float initialTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        MovementStraight straight = new MovementStraight();
        straight.StartPosition = transform.position;
        straight.StartVelocity = new Vector2(0, 1);
        straight.StartTime = 0;

        MovementParabola parabola = new MovementParabola();

        MovementStraight straight2 = new MovementStraight();

        MovementParabola parabola2 = new MovementParabola(1);

        MovementParabola parabola3 = new MovementParabola();

        MovementStraight straight3 = new MovementStraight();

        AddActionStrategy(straight, 0);
        AddActionStrategy(straight3, 11);
        AddActionStrategy(parabola3, 10);
        AddActionStrategy(parabola2, 8);
        AddActionStrategy(straight2, 5);
        AddActionStrategy(parabola, 3);
    }
    /*
        initialTime = Time.time;
        time = 0;
    }
    public bool paused;
    public void Update()
    {
        if (!paused)
            time += Time.deltaTime;
        MyUpdate();
    }
    //*/
}
