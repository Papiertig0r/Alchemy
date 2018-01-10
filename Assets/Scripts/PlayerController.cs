using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject target;
    public static PlayerController player;
    public float speed;
    public float range;
    public float lerpingSpeed;

    public delegate void OnInventoryDown();
    public OnInventoryDown onInventoryDown;

    public Inventory inventory;

    private Animator animator;
    private Vector3 targetOffset;

    private bool executedAttack = false;
    // Use this for initialization
    void Start ()
    {
        if(player == null)
        {
            player = this;
        }
        if(player != this)
        {
            Destroy(this.gameObject);
        }

        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        targetOffset = target.transform.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement();

        if(target.activeSelf)
        {
            Target();
        }

        HandleInput();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 translation = new Vector3(x, y, 0f);
        animator.SetBool("isWalking", translation.magnitude > 0f);
        if (translation.magnitude > 0f)
        {
            if(!target.activeSelf)
            {
                animator.SetFloat("x", x);
                animator.SetFloat("y", y);
            }

            transform.Translate(translation /*.normalized * translation.magnitude*/ * Time.deltaTime * speed);
        }
    }

    private void HandleInput()
    {
        if(Input.GetAxis("Target") != 0f)
        {
            target.SetActive(true);
        }
        else
        {
            target.SetActive(false);
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

        if (translation.magnitude > 0f)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
        }

        target.transform.localPosition = translation * range + targetOffset;
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
