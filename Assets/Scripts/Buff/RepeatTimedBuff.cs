using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Repeat Timed Buff", menuName = "Alchemy/Buffs/Repeat Timed Buff")]
public class RepeatTimedBuff : TimedBuff
{
    public float repeatRate;

    public float minRepeatRate;
    public float maxRepeatRate;
    private float _repeatRate;

    public RepeatTimedBuff(float value, float duration, float repeatRate) : base(value, duration)
    {
        this.repeatRate = repeatRate;
        this._repeatRate = repeatRate;
    }

    public override void Set(float baseValue, float concentration, float purity, float yield)
    {
        float finalBuff = concentration;
        finalBuff *= baseValue;
        value = finalBuff;

        float finalDuration = Buff.Map(yield, 0f, 100f, minDuration, maxDuration);
        duration = finalDuration;

        float finalRepeatRate = Buff.Map(purity, 0f, 100f, maxRepeatRate, minRepeatRate);
        repeatRate = finalRepeatRate;

        Debug.Log(value);
        Debug.Log(duration);
        Debug.Log(repeatRate);
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
