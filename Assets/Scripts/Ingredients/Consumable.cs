using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IConsumable
{
    public Buff buff;
    public float strength = 10f;
    public void Consume(CharaController target)
    {
        buff.Apply(target, strength);
    }
}
