using UnityEngine;

public class PlayerController : CharaController
{
    public static PlayerController player;

    public GameObject target;

    public GameObject attackCollider;

    public Inventory inventory;

    public delegate void OnInventoryDown();
    public OnInventoryDown onInventoryDown;

    private Vector3 targetOffset;

    private bool executedAttack = false;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        if(player == null)
        {
            player = this;
        }
        if(player != this)
        {
            Destroy(this.gameObject);
        }

        attackCollider.GetComponent<AttackCollider>().onHit += Hit;

        targetOffset = target.transform.localPosition;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        if(target.activeSelf)
        {
            Target();
        }

        HandleInput();
    }

    protected override Vector3 CalculateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        return new Vector3(x, y, 0f);
    }

    private void HandleInput()
    {
        if(Input.GetAxis("Target") != 0f)
        {
            target.SetActive(true);
            isTargeting = true;
        }
        else
        {
            target.SetActive(false);
            isTargeting = false;
            target.transform.localPosition = Vector3.zero + targetOffset;
        }

        if(Input.GetAxis("Attack") != 0f)
        {
            if(!executedAttack)
            {
                if (target.activeSelf)
                {
                    RangedAttack();
                }
                else
                {
                    MeleeAttack();
                }
                executedAttack = true;
            }
        }
        else
        {
            executedAttack = false;
        }
    }

    private void Target()
    {
        float x = Input.GetAxis("TargetHorizontal");
        float y = Input.GetAxis("TargetVertical");

        Vector3 translation = new Vector3(x, y, 0f);

        //! \bug when releasing the stick, the enemy defaults to left
        DetectLookingDirection(translation.x);
        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
        }

        target.transform.localPosition = translation * stats.range + targetOffset;
    }

    private void RangedAttack()
    {
        Debug.Log("Ranged attack");
    }

    private void MeleeAttack()
    {
        float attack = Random.Range(0, animator.GetFloat("attackCount"));
        animator.SetFloat("currentAttack", attack);
        animator.SetTrigger("attack");

        float x = animator.GetFloat("x");
        float y = animator.GetFloat("y");

        Vector3 direction = new Vector3(x, y, 0f);
        float angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        attackCollider.transform.SetPositionAndRotation(attackCollider.transform.position, Quaternion.Euler(0f, 0f, angle));
        attackCollider.SetActive(true);
        Invoke("DeactivateAttackCollider", 1f);
    }

    private void DeactivateAttackCollider()
    {
        attackCollider.SetActive(false);
    }

    public void Hit(Collision2D coll)
    {
        EnemyController enemy = coll.collider.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.TakeDamage(stats.attack);
        }
    }
}
