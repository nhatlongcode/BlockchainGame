using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShootBasic : ISkill
{
    public BasicProjectile prefab;
    // Skill start on cooldown
    public float cooldownRemaining { get => ShotTimeline.Top() - History.Inst.time + 4; }
    public const float cooldownTime = 4;

    TimelineList<BasicProjectile> ShotTimeline = new TimelineList<BasicProjectile>();

    public override void Action(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        if (cooldownRemaining <= 0 && this.checkInput(Command.Enter))
        {
            BasicProjectile obj = Instantiate(prefab, transform.position, transform.rotation);

            ShotTimeline.Add(time, obj);
        }
    }

    public override bool KeyProcess(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        return this.checkInput(Command.Space) || this.checkInput(Command.Enter);
    }

    public override bool OnExitSkill(float time)
    {
        return cooldownRemaining > 0;
    }
}
