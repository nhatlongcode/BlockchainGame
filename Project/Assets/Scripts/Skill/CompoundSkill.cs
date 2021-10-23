using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundSkill : ISkill
{
    public List<ISkill> componentSkills = new List<ISkill>();
    public string notes;

    public override void Action(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        foreach (var item in componentSkills)
            item.Action(time,deltaTime, rigidbody, transform);
    }

    public override bool KeyProcess(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        bool result = false;
        foreach (var item in componentSkills)
            result |= item.KeyProcess(time, deltaTime, rigidbody, transform);
        return result;
    }

    public override bool OnExitSkill(float time)
    {
        bool result = true;
        foreach (var item in componentSkills)
            result &= item.OnExitSkill(time);
        return result;
    }

    public override void SyncData()
    {
        foreach (var item in componentSkills)
            item.SyncData();
    }
}
