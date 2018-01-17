using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : Throwable
{
    public float attackStrength = 10f;

    public override void Scale(Vector3 direction)
    {
    }

    protected override void Hit(IHittable hittable)
    {
        hittable.TakeDamage(attackStrength);
    }
}
