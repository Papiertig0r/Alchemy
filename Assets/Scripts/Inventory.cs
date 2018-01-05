using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public int slotCount = 6;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public void Display(bool display)
    {
        inventoryUI.SetActive(display);
    }

    public bool isUiActive
    {
        get
        {
            return inventoryUI.active;
        }
    }

    public bool AddItem(Item item)
    {
        if(itemSlots.Count == slotCount)
        {
            return false;
        }

        ItemSlot slot = itemSlots.Find(x => x.item.name == item.name);
        if(slot == null)
        {
            itemSlots.Add(new ItemSlot(item));
        }
        else
        {
            slot.quantity++;
        }

        inventoryUI.UpdateInventoryUI(itemSlots);
        return true;
    }

    public void RemoveItem(Item item)
    {

    }
}
