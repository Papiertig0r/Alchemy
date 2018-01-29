using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private StateController.OnActionButtonDown savedAction;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            savedAction = StateController.instance.worldAction;
            StateController.instance.worldAction = Interact;
            Entered();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            StateController.instance.worldAction = savedAction;
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
