using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour, IRangedWeapon
{
    public float speed = 1f;
    public float maxScaleMultiplier = 1f;
    public Vector3 rotation;

    protected Vector3 targetPosition;
    protected Quaternion originalRotation;
    protected IRangedAttackCaster originalCaster;

    private AudioSource oneShotSource;
    private bool isThrown = false;
    private float originalDistanceToTarget;

    // Update is called once per frame
    void Update ()
    {
        if(isThrown)
        {
            Vector3 direction = targetPosition - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * speed, Space.World);

            direction = targetPosition - transform.position;

            Rotate();
            Scale(direction);

            if (direction.magnitude < 0.1f)
            {
                Land();
            }
        }
    }

    public virtual void Rotate()
    {
        transform.Rotate(rotation, Space.Self);
    }

    public virtual void Scale(Vector3 direction)
    {
        float scaleMultiplier = Mathf.Sin((1 - (direction.magnitude / originalDistanceToTarget)) * Mathf.PI) * maxScaleMultiplier;
        transform.localScale = Vector3.one + new Vector3(scaleMultiplier, scaleMultiplier);
    }

    public void RangedAttack(Vector3 targetPosition, float accuracy, IRangedAttackCaster caster)
    {
        this.originalCaster = caster;
        this.transform.SetParent(ItemPool.itemPool.transform);
        this.gameObject.SetActive(true);
        Vector2 randomArea = Random.insideUnitCircle * accuracy;
        this.targetPosition = targetPosition + new Vector3(randomArea.x, randomArea.y, 0f);
        isThrown = true;
        originalDistanceToTarget = (this.targetPosition - transform.position).magnitude;
        originalRotation = transform.rotation;

        if (oneShotSource == null)
        {
            oneShotSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Land()
    {
        isThrown = false;
        transform.localScale = Vector3.one;
        transform.rotation = originalRotation;
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        IHittable hittable = coll.GetComponent<IHittable>();
        IRangedAttackCaster caster = coll.GetComponent<IRangedAttackCaster>();
        if(isThrown && hittable != null && caster != originalCaster)
        {
            Hit(hittable);
            Land();
        }
    }

    protected virtual void Hit(IHittable hittable)
    {

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (isThrown)
        {
        }
    }
}
