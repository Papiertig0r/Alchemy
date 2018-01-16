using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Alchemy/Buffs/Current Health")]
public class BuffCurrentHealth : Buff
{
    public override void Apply(Stats stat, float value)
    {
        stat.health += value;
    }
}
