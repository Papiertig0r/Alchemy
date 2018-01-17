using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedAttackEffect
{
    public Applyable applyable;

    public void Apply(CharaController chara)
    {
        Debug.Log("Applied " + applyable.name + " to " + chara.name);
        applyable.Apply(chara);
    }
}
