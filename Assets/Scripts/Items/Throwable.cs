using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour, IRangedWeapon
{
    public float speed = 1f;
    public float maxScaleMultiplier = 1f;
    public bool rotateTowardTarget = false;
    public bool scalesDuringThrow = true;
    public Vector3 rotation;

    public RangedAttackEffect onHitEvent;
    public RangedAreaAttackEffect onLandEvent;

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
            if(scalesDuringThrow)
            {
                Scale(direction);
            }

            if (direction.magnitude < 0.1f)
            {
                Land();
            }
        }
    }

    public virtual void Rotate()
    {
        if (rotateTowardTarget)
        {
            Vector3 direction = targetPosition - transform.position;
            float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
            transform.rotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(0f, 0f, angle));
        }
        else
        {
            transform.Rotate(rotation, Space.Self);
        }
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
        if(onLandEvent.applyable != null)
        {
            onLandEvent.Apply(transform.position);
        }
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
            CharaController chara = coll.GetComponent<CharaController>();
            if(onHitEvent.applyable != null && chara != null)
            {
                onHitEvent.Apply(chara);
            }
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
