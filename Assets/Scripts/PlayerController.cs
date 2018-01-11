using UnityEngine;

public class PlayerController : CharaController
{
    public GameObject target;
    public static PlayerController player;

    public delegate void OnInventoryDown();
    public OnInventoryDown onInventoryDown;

    public Inventory inventory;

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
        spriteRenderer.flipX = translation.x > 0f;
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
        Debug.Log("Melee attack");
    }
}
