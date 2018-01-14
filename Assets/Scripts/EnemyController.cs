using UnityEngine;

public class EnemyController : CharaController
{

    public bool isTethered;
    public float tetherRange;

    public float idleTime = 3f;
    public float targetDistance = 0.1f;

    public bool corpseIsContainer;

    public float attackDelay = 0.5f;
    public float attackRange = 0.1f;
    public float attackCoolDown = 0.5f;

    private AggroCollider aggroCollider;
    [SerializeField]
    private GameObject attackCollider;

    private bool isAggro = false;
    private bool attackCoolsDown = false;

    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool targetisGoingToBeSet = false;

    protected override void Start ()
    {
        base.Start();

        aggroCollider = GetComponentInChildren<AggroCollider>();
        aggroCollider.Setup(boxCollider.offset, stats.range);
        aggroCollider.onAggro += OnAggro;
        aggroCollider.onDeaggro += OnDeaggro;

        attackCollider.GetComponent<AttackCollider>().onHit += Hit;

        startingPosition = transform.position;
        SetTarget();
    }

    // Maybe abstract to Character controller w/ protected keyword
    protected override void Update ()
    {
        if((targetPosition - transform.position).magnitude < attackRange && isAggro)
        {
            Attack();
        }
        else if(!attackCoolsDown)
        {
            base.Update();
        }

        if(!isTethered)
        {
            startingPosition = transform.position;
        }

        if(isAggro)
        {
            targetPosition = PlayerController.player.transform.position;
        }
    }

    protected override Vector3 CalculateMovement()
    {
        Vector3 translation = targetPosition - transform.position;
        if (translation.magnitude < targetDistance)
        {
            if (!targetisGoingToBeSet)
            {
                targetisGoingToBeSet = true;
                Invoke("SetTarget", idleTime);
            }
            translation = Vector3.zero;
        }
        return translation.normalized;
    }

    private void SetTarget()
    {
        Vector2 randomTarget = Random.insideUnitCircle * tetherRange;
        targetPosition = new Vector3(randomTarget.x, randomTarget.y, 0f);
        targetPosition += startingPosition;
        targetisGoingToBeSet = false;
    }

    private void OnAggro(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player != null)
        {
            isAggro = true;
            targetPosition = PlayerController.player.transform.position;
        }
    }

    private void OnDeaggro(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player != null)
        {
            isAggro = false;
            targetPosition = player.transform.position;
            startingPosition = transform.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(startingPosition, tetherRange);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, tetherRange);
        }
    }

    protected override void Die()
    {
        base.Die();

        Destroy(this.gameObject);
        //Create container
        //Container container = new Container();
    }

    public void Hit(Collision2D coll)
    {
        DeactivateAttackCollider();
        PlayerController player = coll.collider.GetComponent<PlayerController>();
        if(player != null)
        {
            player.TakeDamage(stats.attack);
        }
    }

    private void Attack()
    {
        animator.SetBool("isWalking", false);
        if (attackCoolsDown)
        {
            return;
        }
        attackCoolsDown = true;

        Vector3 direction = targetPosition - transform.position;
        DetectLookingDirection(direction.x);
        spriteRenderer.flipX = isLookingRight;
        float attack = Random.Range(0, animator.GetFloat("attackCount"));
        animator.SetFloat("currentAttack", attack);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);

        float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        attackCollider.transform.SetPositionAndRotation(attackCollider.transform.position, Quaternion.Euler(0f, 0f, angle));
        Invoke("ActivateAttackCollider", attackDelay);
    }

    private void ActivateAttackCollider()
    {
        attackCollider.SetActive(true);
        oneShotSource.PlayOneShot(hitClip);
        animator.SetTrigger("attack");
        Invoke("DeactivateAttackCollider", attackCoolDown);
    }

    private void DeactivateAttackCollider()
    {
        attackCollider.SetActive(false);
        attackCoolsDown = false;
    }

}
