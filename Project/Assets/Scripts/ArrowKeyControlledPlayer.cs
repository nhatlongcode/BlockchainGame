using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Command
{
    Up, Down, Left, Right, Space, Enter,
}

public class ArrowKeyControlledPlayer : HistoryObject
{
    enum State
    {
        OnLadder = 1,           // TODO: Add snap to ladder feature
        OnGround = 2,           // TODO: Add snap to ground feature

    }
    State state;

    public int currentSkill = 0;
    
    public ISkill[] SkillList = new ISkill[4];

    public new Collider2D collider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (collider == null)
            collider = GetComponent<Collider2D>();
    }

    public const float accel = 10f;
    public bool replayMode = false;
    // Update is called once per frame
    public override void MyUpdate()
    {
        if (SkillList[currentSkill].KeyProcess(time, deltaTime, rigidbody, transform) || replayMode)
        {
            History.Inst.StartTime();
            SkillList[currentSkill].Action(time, deltaTime, rigidbody, transform);
        }
        else
        {
            SkillList[currentSkill].Action(time, deltaTime, rigidbody, transform);
            History.Inst.StopTime();
        }
    }
}