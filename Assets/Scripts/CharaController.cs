using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class CharaController : MonoBehaviour
{
    public Stats stats;
    public GameObject bloodParticle;

    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected bool isTargeting = false;
    protected bool isRunning = false;
    protected bool isLookingRight = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
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

        HandleMovement(translation);
    }

    private void HandleMovement(Vector3 translation)
    {
        animator.SetBool("isWalking", translation.magnitude > 0f);
        //! \bug when releasing the stick, the enemy defaults to left
        DetectLookingDirection(translation.x);
        spriteRenderer.flipX = isLookingRight;
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

    public void TakeDamage(float attack)
    {
        Instantiate<GameObject>(bloodParticle, this.transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y), Quaternion.identity );
        stats.health -= attack;
    }

    protected virtual void Die()
    {
        Debug.Log("Ded");
    }

    protected void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Collision!");
    }
}
