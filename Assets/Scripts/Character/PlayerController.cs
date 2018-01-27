using UnityEngine;

public class PlayerController : CharaController
{
    public static PlayerController player;

    public AudioClip walking;

    public GameObject target;
    //public AreaEffect areaOfEffectDisplayer;

    public GameObject attackCollider;

    public Inventory inventory;

    public delegate void OnActioButtonDown();
    public OnActioButtonDown onActionButtonDown;

    private Vector3 targetOffset;
    private bool executedAttack = false;
    private Vector3 velocity;

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

        onActionButtonDown = inventory.ConsumeHotbarItem;

        attackCollider.GetComponent<AttackCollider>().onHit += Hit;
        loopSource = GetComponent<AudioSource>();

        targetOffset = target.transform.localPosition;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();

        if(CalculateMovement().magnitude > 0f && !loopSource.isPlaying)
        {
            loopSource.loop = true;
            loopSource.clip = walking;
            loopSource.Play();
        }
        else if(CalculateMovement().magnitude == 0f)
        {
            loopSource.Stop();
        }

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
        HandleTargeting();

        HandleAttacking();

        HandleActions();
    }

    private void HandleTargeting()
    {
        if (Input.GetAxis("Target") != 0f)
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
    }

    private void HandleAttacking()
    {
        if (Input.GetAxis("Attack") != 0f)
        {
            if (!executedAttack)
            {
                if (target.activeSelf)
                {
                    CastRangedAttack();
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

    private void HandleActions()
    {
        if(Input.GetButtonDown("Action") && onActionButtonDown != null)
        {
            onActionButtonDown.Invoke();
        }
    }

    private void Target()
    {
        float x = Input.GetAxis("TargetHorizontal");
        float y = Input.GetAxis("TargetVertical");

        Vector3 translation = new Vector3(x, y, 0f);

        Vector3 smoothTranslation = Vector3.SmoothDamp(target.transform.localPosition, translation * stats.GetStatValue(StatType.RANGE) + targetOffset, ref velocity, 0.1f);

        DetectLookingDirection(translation.x);
        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
        }

        target.transform.localPosition = smoothTranslation;
    }

    public override void CastRangedAttack()
    {
        Item item = inventory.GetHotbarItem();
        if(item != null)
        {
            IRangedWeapon weapon = item.GetComponent<IRangedWeapon>();
            if(weapon != null)
            {
                weapon = inventory.InstantiateItem(item).GetComponent<IRangedWeapon>();
                weapon.RangedAttack(target.transform.position, stats.GetStatValue(StatType.ACCURACY), this);
                inventory.RemoveItemForRangedAttack();
            }
            else
            {
                Destroy(item.gameObject);
            }
        }
    }

    private void MeleeAttack()
    {
        oneShotSource.PlayOneShot(hitClip);

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
        DeactivateAttackCollider();
        EnemyController enemy = coll.collider.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.TakeDamage(stats.GetStatValue(StatType.ATTACK));
        }
    }
}
