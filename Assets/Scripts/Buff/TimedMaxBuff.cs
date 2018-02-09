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
        Stat stat = stats.GetStat(statType);
        Register(stats);
        stat.buffMax.Add(this);
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            Stat stat = stats.GetStat(statType);
            stat.buffMax.Remove(this);
            Unregister();
        }
    }

    public override string ToString()
    {
        return buffType.ToString() + "S maximum " + statType.ToString() + " by " + Mathf.Abs(value).ToString("F0") + " for " + duration.ToString("F1") + "s";
    }
}
