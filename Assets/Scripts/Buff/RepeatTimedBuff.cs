using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Repeat Timed Buff", menuName = "Alchemy/Buffs/Repeat Timed Buff")]
public class RepeatTimedBuff : TimedBuff
{
    public float repeatRate;
    private float _repeatRate;

    public RepeatTimedBuff(float value, float duration, float repeatRate) : base(value, duration)
    {
        this.repeatRate = repeatRate;
        this._repeatRate = repeatRate;
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;
        _repeatRate -= Time.deltaTime;
        if(_repeatRate < 0f)
        {
            Stat stat = stats.GetStat(type);
            stat += value;
            _repeatRate = repeatRate;
        }

        if (duration < 0f)
        {
            Unregister();
        }
    }
}
