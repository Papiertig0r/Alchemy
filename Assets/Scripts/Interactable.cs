using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    PlayerController.OnActioButtonDown savedAction;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            savedAction = player.onActionButtonDown;
            player.onActionButtonDown = Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            player.onActionButtonDown = savedAction;
        }
    }

    public abstract void Interact();
}
