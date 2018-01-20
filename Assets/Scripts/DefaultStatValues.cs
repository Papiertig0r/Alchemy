using System.Collections.Generic;
using UnityEngine;

public static class DefaultStatValues
{
    public static Dictionary<StatType, Stat> defaultStatValues = new Dictionary<StatType, Stat>
    {
        {StatType.HEALTH,                   new Stat(0, 50) },
        {StatType.MANA,                     new Stat(0, 50) },
        {StatType.HEALTH_REGEN,             new Stat(Mathf.NegativeInfinity, Mathf.Infinity) },
        {StatType.MANA_REGEN,               new Stat(Mathf.NegativeInfinity, Mathf.Infinity) },
        {StatType.SPEED,                    new Stat(0, Mathf.Infinity) },
        {StatType.RUNNING_SPEED,            new Stat(0, Mathf.Infinity) },
        {StatType.SPEED_DURING_TARGETING,   new Stat(0, Mathf.Infinity) },
        {StatType.RANGE,                    new Stat(0, Mathf.Infinity) },
        {StatType.ATTACK,                   new Stat(0, Mathf.Infinity) },
        {StatType.DEFENSE,                  new Stat(0, Mathf.Infinity) },
        {StatType.ACCURACY,                 new Stat(0, Mathf.Infinity) }
    };
}