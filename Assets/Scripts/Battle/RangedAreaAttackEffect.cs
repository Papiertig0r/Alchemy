using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedAreaAttackEffect
{
    public Applyable applyable;

    public float radius;
    public virtual void Apply(Vector3 position)
    {
        Vector2 pos = new Vector2(position.x, position.y);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, radius, pos);

        foreach (RaycastHit2D hit in hits)
        {
            CharaController chara = hit.collider.GetComponent<CharaController>();

            if (chara != null)
            {
                applyable.Apply(chara);
            }
        }
    }
}