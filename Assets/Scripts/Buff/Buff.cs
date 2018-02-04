using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Alchemy/Buffs/Buff")]
[System.Serializable]
public class Buff : ScriptableObject
{
    public new string name;
    public StatType type;
    public float value;

    public Buff(float value)
    {
        this.value = value;
    }

    public Buff(string name, StatType type, float value)
    {
        this.name = name;
        this.type = type;
        this.value = value;
    }

    public virtual void Set(float baseValue, float concentration, float purity, float yield)
    {
        float finalBuff = concentration;
        finalBuff += purity / 100f;
        finalBuff += yield / 100f;
        finalBuff *= baseValue;
        value = finalBuff;
    }

    public virtual void Apply(Stats stats)
    {
        Stat stat2 = stats.GetStat(type);
        stat2 += value;
    }

    public static float Map(float value, float start, float stop, float newStart, float newStop)
    {
        value = Mathf.Clamp(value, start, stop);
        float oldSlope = stop - start;
        float newSlope = newStop - newStart;
        float newValue = (value - start) / oldSlope * newSlope + newStart;
        return newValue;
    }

    public virtual string ToString(float baseValue, float concentration, float purity, float yield)
    {
        Set(baseValue, concentration, purity, yield);
        string identifier = "Increases";
        if(value < 0)
        {
            identifier = "Decreases";
        }
        return identifier + " " + type.ToString() + " by " + Mathf.Abs(value).ToString("F0");
    }
}
