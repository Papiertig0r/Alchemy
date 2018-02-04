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

    public override string ToString(float baseValue, float concentration, float purity, float yield)
    {
        Set(baseValue, concentration, purity, yield);
        string identifier = "Increases";
        if (value < 0)
        {
            identifier = "Decreases";
        }
        return identifier + " maximum " + type.ToString() + " by " + Mathf.Abs(value).ToString("F0") + " for " + duration.ToString("F1") + "s";
    }
}
