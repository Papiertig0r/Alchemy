using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class CharaController : MonoBehaviour, IHittable, IRangedAttackCaster
{
    public Stats stats;
    public GameObject bloodParticle;

    //Change this to a sound almanach
    public AudioClip takeDamageClip;
    public AudioClip hitClip;

    public bool canMove = true;

    protected Animator animator;
    protected AudioSource loopSource;
    protected AudioSource oneShotSource;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected bool isTargeting = false;
    // Running is not implemented yet!
    protected bool isRunning = false;
    protected bool isLookingRight = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        loopSource = gameObject.AddComponent<AudioSource>();
        oneShotSource = gameObject.AddComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = Instantiate<Stats>(stats);

        stats.Start();
        stats.health.fallBelow += Die;
    }

    protected virtual void Update()
    {
        stats.Update();

        Vector3 translation = CalculateMovement();

        if(canMove)
        {
            HandleMovement(translation);
        }
    }

    private void HandleMovement(Vector3 translation)
    {
        animator.SetBool("isWalking", translation.magnitude > 0f);
        spriteRenderer.flipX = isLookingRight;

        if(!isTargeting)
        {
            DetectLookingDirection(translation.x);
        }

        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", translation.x);
            animator.SetFloat("y", translation.y);
            float speed = stats.speed;
            if(isTargeting)
            {
                speed = stats.targetingSpeed;
            }
            else if(isRunning)
            {
                speed = stats.runningSpeed;
            }
            transform.Translate(translation * Time.deltaTime * speed);
        }
    }

    protected void DetectLookingDirection(float x)
    {
        if (x > 0f)
        {
            isLookingRight = true;
        }
        else if (x < 0f)
        {
            isLookingRight = false;
        }
    }

    protected abstract Vector3 CalculateMovement();

    public virtual void TakeDamage(float attack)
    {
        Instantiate<GameObject>(bloodParticle, this.transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y), Quaternion.identity );
        oneShotSource.PlayOneShot(takeDamageClip);

        stats.health -= attack;
    }

    protected virtual void Die()
    {
    }

    protected void OnCollisionEnter2D(Collision2D coll)
    {
    }

    public virtual void CastRangedAttack()
    {

    }
}
