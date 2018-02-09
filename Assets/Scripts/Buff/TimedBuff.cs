using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timed Buff", menuName = "Alchemy/Buffs/Timed Buff")]
public class TimedBuff : Buff
{
    public float duration;
    public float minDuration;
    public float maxDuration;
    protected Stats stats;

    public TimedBuff(float value, float duration) : base(value)
    {
        this.duration = duration;
    }

    public override void Apply(Stats stats)
    {
        Register(stats);
        Stat stat = stats.GetStat(statType);
        stat += value;

    }

    public override void Set(float baseValue, float concentration, float purity, float yield)
    {
        float finalBuff = concentration;
        finalBuff += purity / 200f;
        finalBuff *= baseValue;
        value = finalBuff;

        yield += purity / 200f;
        float finalDuration = Buff.Map(yield, 0f, 100f, minDuration, maxDuration);
        duration = finalDuration;
    }

    public virtual void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            Stat stat = stats.GetStat(statType);
            stat -= value;
            Unregister();
        }
    }

    protected void Register(Stats stats)
    {
        this.stats = stats;
        stats.activeBuffs.Add(this);
        stats.onUpdate += OnUpdate;
    }

    protected void Unregister()
    {
        stats.onUpdate -= OnUpdate;
        stats.activeBuffs.Remove(this);
    }

    public override string ToString()
    {
        return buffType.ToString() + "S " + statType.ToString() + " by " + Mathf.Abs(value).ToString("F0") + " for " + duration.ToString("F1") + "s";
    }
}
