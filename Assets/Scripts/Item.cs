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
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            player.onInventoryDown += PickUp;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            player.onInventoryDown -= PickUp;
        }
    }

    private void PickUp()
    {
        PlayerController player = PlayerController.player;
        if (player.inventory.AddItem(this))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(player.transform);
        }
    }
}
