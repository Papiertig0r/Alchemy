using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timed Max Buff", menuName = "Alchemy/Buffs/Timed Max Buff")]
public class TimedMaxBuff : TimedBuff
{
    public TimedMaxBuff(float value, float duration) : base(value, duration)
    {

    }

    public override void Apply(Stats stats)
    {
        this.stats = stats;
        Stat stat = stats.GetStat(type);
        Register(stats);
        stat.buffMax.Add(this);
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            Stat stat = stats.GetStat(type);
            stat.buffMax.Remove(this);
            Unregister();
        }
    }
}
