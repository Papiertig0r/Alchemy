using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class CharaController : MonoBehaviour
{
    protected Stats stats;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        Vector3 translation = CalculateMovement();

        HandleMovement(translation);
    }

    private void HandleMovement(Vector3 translation)
    {
        animator.SetBool("isWalking", translation.magnitude > 0f);
        //! \bug when releasing the stick, the enemy defaults to left
        spriteRenderer.flipX = translation.x > 0f;
        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", translation.x);
            animator.SetFloat("y", translation.y);

            transform.Translate(translation * Time.deltaTime * stats.speed);
        }
    }

    protected abstract Vector3 CalculateMovement();
}
