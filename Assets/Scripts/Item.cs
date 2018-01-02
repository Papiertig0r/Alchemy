using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;
    private bool canBePickedUp = false;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

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
            if(PlayerController.player.inventory.AddItem(this))
            {
                canBePickedUp = false;
                gameObject.SetActive(false);
            }
        }
    }
}
