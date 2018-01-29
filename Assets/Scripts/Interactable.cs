using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    private StateController.OnActionButtonDown savedAction;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            savedAction = StateController.instance.worldAction;
            StateController.instance.worldAction = Interact;
            Entered();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            StateController.instance.worldAction = savedAction;
            Left();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Coll enter");
        OnTriggerEnter2D(coll.collider);
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log("Coll exit");
        OnTriggerExit2D(coll.collider);
    }

    protected virtual void Entered()
    {

    }

    protected virtual void Left()
    {

    }

    public abstract void Interact();
}
