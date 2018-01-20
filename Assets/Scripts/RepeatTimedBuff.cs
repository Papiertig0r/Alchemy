using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            stat += value;
            _repeatRate = repeatRate;
        }

        if (duration < 0f)
        {
            stats.onUpdate -= OnUpdate;
            stats.activeBuffs.Remove(this);
        }
    }
}
