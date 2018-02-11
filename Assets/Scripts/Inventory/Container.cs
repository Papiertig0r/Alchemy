using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public List<Item> items;
    [SerializeField]
    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        inventory.SetUi(UIManager.containerUI);
        inventory.InitItemSlots();
        foreach (Item item in items)
        {
            Item newItem = Inventory.InstantiateItem(item, transform);
            inventory.AddItem(newItem);
        }
    }

    public override void Interact()
    {
        Open();
    }

    private void Open()
    {
        UIManager.containerUI.SetUp(inventory.numberOfSlots);
        inventory.UpdateUi();
        UIManager.containerUI.Activate();
    }

    protected override void Left()
    {
        UIManager.containerUI.Deactivate();
    }
}
