using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using System;
using ExitGames.Client.Photon;

public class SkillMovementBasic : ISkill
{
    public const float accel = Constant.baseCharMoveSpeed;
    
    TimelineList<Vector2> movementCommands = new TimelineList<Vector2>();

    Vector2 direction(Command dir)
    {
        switch (dir)
        {
            case Command.Up:
                return new Vector2(0, 1);
            case Command.Down:
                return new Vector2(0, -1);
            case Command.Left:
                return new Vector2(-1, 0);
            case Command.Right:
                return new Vector2(1, 0);
            default:
                return new Vector2(0, 0);
        }
    }

    public override void Action(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        rigidbody.AddForce(movementCommands.GetAction(time) * deltaTime * accel);
    }

    public override bool KeyProcess(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        Vector2 dir = new Vector2(0, 0);
        bool anyMovementKeyPressed = false;
        List<Command> movementCommandList = new List<Command>
        {
            Command.Up,
            Command.Down,
            Command.Left,
            Command.Right,
            Command.Space,
            Command.Enter
        };

        foreach (var key in movementCommandList)
        {
            if (this.checkInput(key))
            {
                dir += direction(key);
                anyMovementKeyPressed = true;
            }
        }

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        if (anyMovementKeyPressed)
            movementCommands.Add(time, dir);

        return anyMovementKeyPressed;
    }

    public override bool OnExitSkill(float time)
    {
        movementCommands.Add(time, new Vector2(0, 0));
        return true;
    }

    public float EnemyReletiveTime;
    public override void SyncData()
    {
        List<Event<Vector2>> items = movementCommands.AllEventsAfterTime(EnemyReletiveTime);
        items.Sort();
        Event<Vector2>[] data = items.ToArray();

        if (data.Length != 0)
            photonView.RPC("AcceptSyncData_MovementBasic", RpcTarget.Others, (object)data);

        EnemyReletiveTime = History.Inst.time;
    }

    [PunRPC]
    protected virtual void AcceptSyncData_MovementBasic(Event<Vector2>[] data)
    {
        for (int i = 0; i < data.Length; i++)
            movementCommands.Add(data[i].Item1, data[i].Item2);
    }

    static SkillMovementBasic()
    {
        GameManager.SerializeTypesToRegister.Add(typeof(Event<Vector2>));
    }
}
