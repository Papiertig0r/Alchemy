using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroCollider : MonoBehaviour
{
    public delegate void OnAggroHandler(Collider2D coll);
    public OnAggroHandler onAggro;
    public OnAggroHandler onDeaggro;

    public void Setup(Vector2 offset, float radius)
    {
        CircleCollider2D aggroCollider = GetComponent<CircleCollider2D>();
        aggroCollider.offset = offset;
        aggroCollider.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        onAggro.Invoke(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        onDeaggro.Invoke(coll);
    }
}
