using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMaxBuff : TimedBuff
{
    public TimedMaxBuff(float value, float duration) : base(value, duration)
    {

    }

    public override void Apply(Stats stats, Stat stat)
    {
        this.stats = stats;
        this.stat = stat;
        stats.activeBuffs.Add(this);
        stats.onUpdate += OnUpdate;
        stat.buffMax.Add(this);
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            stat.buffMax.Remove(this);
            stats.onUpdate -= OnUpdate;
            stats.activeBuffs.Remove(this);
        }
    }
}
