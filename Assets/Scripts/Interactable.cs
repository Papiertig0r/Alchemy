using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private PlayerController.OnActioButtonDown savedAction;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            savedAction = player.onActionButtonDown;
            player.onActionButtonDown = Interact;
            Entered();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            player.onActionButtonDown = savedAction;
            Left();
        }
    }

    protected virtual void Entered()
    {

    }

    protected virtual void Left()
    {

    }

    public abstract void Interact();
}
