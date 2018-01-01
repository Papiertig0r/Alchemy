using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Animator animator;
    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 translation = new Vector3(x, y, 0f);
        animator.SetBool("isWalking", translation.magnitude > 0f);
        if(translation.magnitude > 0f)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);

            transform.Translate(translation.normalized * translation.magnitude * Time.deltaTime * speed);
        }
    }
}
