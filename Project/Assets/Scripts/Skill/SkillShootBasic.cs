using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShootBasic : ISkill
{
    public BasicProjectile prefab;
    // Skill start on cooldown
    public float cooldownRemaining { get => ShotTimeline.Top() - History.Inst.time + 4; }
    public const float cooldownTime = 4;
    public float bulletSpeed = 10;

    TimelineList<BulletProperty> ShotTimeline = new TimelineList<BulletProperty>();
    Dictionary<BulletProperty, BasicProjectile> bulletDictionary 
        = new Dictionary<BulletProperty, BasicProjectile>();

    public override void Action(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        BulletProperty bulletProperty = ShotTimeline.GetAction(time);
        // If there is no corresponding projectile for the latest ShotTimeline
        if (bulletProperty != null && 
            (!bulletDictionary.ContainsKey(bulletProperty) || 
            bulletDictionary[bulletProperty] == null))
        {
            /* TODO: Check validity
            bulletProperty.position = transform.position;
            bulletProperty.velocity = transform.up * bulletSpeed;
            bulletProperty.shootTime = History.Inst.time;
            //*/

            BasicProjectile obj = Instantiate(prefab);
            obj.bulletProperty = bulletProperty;

            bulletDictionary[bulletProperty] = obj;
        }
    }

    public override bool KeyProcess(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform)
    {
        bool result = this.checkInput(Command.Space);

        if (this.checkInput(Command.Enter))
        {
            if (cooldownRemaining <= 0)
            {
                BulletProperty bulletProperty = new BulletProperty();
                bulletProperty.position = transform.position;
                bulletProperty.velocity = transform.up * bulletSpeed;
                bulletProperty.shootTime = History.Inst.time;

                ShotTimeline.Add(time, bulletProperty);
            }
            result = true;
        }

        return result;
    }

    public override bool OnExitSkill(float time)
    {
        return cooldownRemaining > 0;
    }

    public float EnemyReletiveTime;
    public override void SyncData()
    {
        List<Tuple<float, BulletProperty>> data = ShotTimeline.AllEventsAfterTime(EnemyReletiveTime);

        photonView.RPC("AcceptSyncData", RpcTarget.Others, data);

        EnemyReletiveTime = History.Inst.time;
    }

    [PunRPC]
    public virtual void AcceptSyncData(List<Tuple<float, BulletProperty>> data)
    {
        data.Sort();
        foreach (var item in data)
            ShotTimeline.Add(item.Item1, item.Item2);
    }
}
