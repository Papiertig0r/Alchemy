using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ScriptableObject
{
    public GameObject particle;
    public void Apply(CharaController target, float value)
    {
        ApplyBuff(target, value);
        if (value > 0f)
        {
            Instantiate<GameObject>(particle, target.transform);
        }
    }

    protected virtual void ApplyBuff(CharaController target, float value)
    {

    }
}
