using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Timed Buff", menuName = "Alchemy/Buffs/Timed Buff")]
public class TimedBuff : Buff
{
    public float duration;
    protected Stats stats;

    public TimedBuff(float value, float duration) : base(value)
    {
        this.duration = duration;
    }

    public override void Apply(Stats stats)
    {
        Register(stats);
        Stat stat = stats.GetStat(type);
        stat += value;
    }

    public virtual void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            Stat stat = stats.GetStat(type);
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
}
