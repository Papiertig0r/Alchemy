using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Alchemy/Buffs/Current Health")]
public class BuffCurrentHealth : Buff
{
    protected override void ApplyBuff(CharaController target, float value)
    {
        target.stats.health += value;
    }
}
