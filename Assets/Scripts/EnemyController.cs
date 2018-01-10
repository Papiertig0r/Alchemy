using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyController : CharaController
{
    public bool isTethered;
    public float tetherRange;

    public float idleTime = 3f;
    public float targetDistance = 0.1f;
    
    private CircleCollider2D aggroCollider;

    private bool isAggro = false;

    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool targetisGoingToBeSet = false;

    protected override void Start ()
    {
        base.Start();

        aggroCollider = GetComponent<CircleCollider2D>();
        aggroCollider.radius = stats.range;
        aggroCollider.isTrigger = true;

        startingPosition = transform.position;
        SetTarget();
    }

    // Maybe abstract to Character controller w/ protected keyword
    protected override void Update ()
    {
        base.Update();

        if(!isTethered)
        {
            startingPosition = transform.position;
        }
    }

    protected override Vector3 CalculateMovement()
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
