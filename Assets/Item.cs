using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool canBePickedUp = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<PlayerController>())
        {
            canBePickedUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            canBePickedUp = false;
        }
    }

    private void Update()
    {
        if(canBePickedUp && Input.GetButtonDown("Fire1"))
        {
            canBePickedUp = false;
            gameObject.SetActive(false);
        }
    }
}
