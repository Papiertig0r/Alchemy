using UnityEngine;

public interface IRangedWeapon
{
    void RangedAttack(Vector3 targetPosition, float accuracy, IRangedAttackCaster caster);
}