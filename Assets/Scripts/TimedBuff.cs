using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBuff : Buff
{
    public float duration;
    protected Stats stats;
    protected Stat stat;

    public TimedBuff(float value, float duration) : base(value)
    {
        this.duration = duration;
    }

    public override void Apply(Stats stats, Stat stat)
    {
        this.stats = stats;
        this.stat = stat;
        stats.activeBuffs.Add(this);
        stats.onUpdate += OnUpdate;
        stat += value;
        Debug.Log("Applied buff");
    }

    public virtual void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            stat -= value;
            stats.onUpdate -= OnUpdate;
            stats.activeBuffs.Remove(this);

            Debug.Log("Destroyed buff");
        }
    }
}
