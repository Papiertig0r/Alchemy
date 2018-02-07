using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public override void Interact()
    {
        Open();
    }

    private void Open()
    {
        UIManager.containerUI.SetUp(inventory.numberOfSlots);
        UIManager.containerUI.Activate();
    }

    protected override void Left()
    {
        UIManager.containerUI.Deactivate();
    }
}
