using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    // Use this for initialization
    void Start ()
    {
        if (target != null)
        {
            offset = transform.position - target.transform.position;
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset ;
        }
    }
}
