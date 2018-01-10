using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float aggroDistance = 3f;
    public float speed = 2f;

    public bool isTethered;
    public float tetherRange;

    public float idleTime = 3f;
    public float targetDistance = 0.1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D aggroCollider;

    private bool isAggro = false;

    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool targetisGoingToBeSet = false;

    private void Start ()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aggroCollider = GetComponent<CircleCollider2D>();
        aggroCollider.radius = aggroDistance;

        startingPosition = transform.position;
        SetTarget();
    }

    // Maybe abstract to Character controller w/ protected keyword
    private void Update ()
    {
        Vector3 translation = CalculateMovement();

        HandleMovement(translation);

        if(!isTethered)
        {
            startingPosition = transform.position;
        }
    }

    private Vector3 CalculateMovement()
    {
        Vector3 translation = new Vector3(0f, 0f, 0f);
        if (isAggro)
        {
            translation = PlayerController.player.transform.position - transform.position;
        }
        else
        {
            translation = targetPosition - transform.position;
            if (translation.magnitude < targetDistance)
            {
                if (!targetisGoingToBeSet)
                {
                    targetisGoingToBeSet = true;
                    Invoke("SetTarget", idleTime);
                }
                translation = Vector3.zero;
            }
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

    private void HandleMovement(Vector3 translation)
    {
        animator.SetBool("isWalking", translation.magnitude > 0f);
        //! \bug when releasing the stick, the enemy defaults to left
        spriteRenderer.flipX = translation.x > 0f;
        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", translation.x);
            animator.SetFloat("y", translation.y);

            transform.Translate(translation.normalized * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player != null)
        {
            isAggro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player != null)
        {
            isAggro = false;
            targetPosition = transform.position;
            startingPosition = transform.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(startingPosition, tetherRange);
    }
}
