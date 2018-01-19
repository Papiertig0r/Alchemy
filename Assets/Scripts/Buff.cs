using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public float value;

    public Buff(float value)
    {
        this.value = value;

        Debug.Log("Created buff");
    }

    public virtual void Apply(Stats stats, Stat stat)
    {
        stat -= value;
        Debug.Log("Applied buff");
    }
}
