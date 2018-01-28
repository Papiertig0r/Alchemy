using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IConsumable, IShowable, IComparable
{
    public string description;
    public float strength = 10f;
    public float concentration = 10f;
    public float purity = 10f;
    public float yield = 10f;
    public List<Buff> buffs = new List<Buff>();

    public void Consume(CharaController target)
    {
        foreach(Buff buff in buffs)
        {
            Buff newBuff = Instantiate(buff);
            newBuff.Set(strength, concentration, purity, yield);
            newBuff.Apply(target.stats);
        }
    }

    public void DeactivateExtendedInfo()
    {
        UIManager.consumableInfoUi.Deactivate();
    }

    public void ShowExtendedInfo()
    {
        UIManager.consumableInfoUi.SetConsumable(this);
        UIManager.consumableInfoUi.Activate();
    }

    public bool Compare(IComparable other)
    {
        if (other == null)
        {
            return false;
        }

        Consumable otherConsumable = other as Consumable;
        if (otherConsumable == null)
        {
            return false;
        }

        return true;
    }
}
