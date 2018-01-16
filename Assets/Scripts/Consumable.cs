using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public Buff buff;
    public void Consume(CharaController target)
    {
        buff.Apply(target.stats, 10f);
    }
}
