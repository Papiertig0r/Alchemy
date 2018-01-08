using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Effect
{
    public string name;
    public string description;

    public float minConc;
    public float maxConc;
    public float bestConc;
    public int tier = 1;

    public float[] phaseMultiplier = new float[4];

    public float GetPotency(float concentration)
    {
        if(concentration < minConc)
        {
            return 0f;
        }
        if (concentration == bestConc)
        {
            return 1f;
        }
        if(concentration > maxConc)
        {
            return 0f;
        }

        if(minConc <= concentration && concentration < bestConc)
        {
            float pot = (concentration - minConc);
            pot /= (bestConc - minConc);
            pot *= Mathf.PI / 2;
            pot = Mathf.Sin(pot);
            return Mathf.Pow(pot, 2f);
        }

        if (bestConc < concentration && concentration <= maxConc)
        {
            float pot = (concentration - bestConc);
            pot /= (maxConc - bestConc);
            pot *= Mathf.PI / 2;
            pot = Mathf.Cos(pot);
            return Mathf.Pow(pot, 2f);
        }

        return 0f;
    }

    public float Tier()
    {
        return 1/(1 + Mathf.Exp(-tier));
    }

    public float PhaseMultiplier(IngredientType type)
    {
        return phaseMultiplier[(int)type];
    }
}
