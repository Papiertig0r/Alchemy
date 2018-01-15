using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public delegate void OnHitHandler(Collision2D coll);
    public OnHitHandler onHit;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        onHit.Invoke(coll);
    }
}
