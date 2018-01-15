using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : Throwable
{
    public float attackStrength = 10f;
    public bool rotateTowardTarget = false;

    public override void Rotate()
    {
        if(rotateTowardTarget)
        {
            Vector3 direction = targetPosition - transform.position;
            float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
            transform.rotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(0f, 0f, angle));
        }
        else
        {
            base.Rotate();
        }
    }

    public override void Scale(Vector3 direction)
    {
    }

    protected override void Hit(IHittable hittable)
    {
        hittable.TakeDamage(attackStrength);
    }
}
