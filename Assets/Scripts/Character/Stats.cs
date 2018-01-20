using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Alchemy/Stats")]
public class Stats : ScriptableObject
{
    public List<Stat> statList = new List<Stat>();
    public List<Buff> activeBuffs = new List<Buff>();

    public delegate void UpdateHandler();
    public UpdateHandler onUpdate;

    public Stats()
    {
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            Stat stat = new Stat(DefaultStatValues.defaultStatValues[type]);
            stat.type = type;
            statList.Add(stat);
        }
    }

    public void Start()
    {
        GetStat(StatType.HEALTH).Max();
        GetStat(StatType.MANA).Max();
    }

    public void Update()
    {
        ModifyStat(StatType.HEALTH, GetStat(StatType.HEALTH_REGEN) * Time.deltaTime);
        ModifyStat(StatType.MANA, GetStat(StatType.MANA_REGEN) * Time.deltaTime);

        if (onUpdate != null)
        {
            onUpdate.Invoke();
        }
    }

    public Stat GetStat(StatType type)
    {
        return statList.Find(stat => stat.type == type);
    }

    public float GetStatValue(StatType type)
    {
        return GetStat(type).current;
    }

    public void ModifyStat(StatType type, float value)
    {
        Stat stat = GetStat(type);
        stat += value;
    }
}
