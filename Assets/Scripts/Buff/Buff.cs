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

    public virtual void Apply(Stats stats)
    {
        Stat stat2 = stats.GetStat(type);
        stat2 += value;
    }
}
