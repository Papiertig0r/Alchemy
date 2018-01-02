using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }

    public bool AddItem(Item item)
    {
        if(itemSlots.Count == 6)
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

        Debug.Log("Updating inventory UI");
        inventoryUI.UpdateInventoryUI(itemSlots);
        return true;
    }

    public void RemoveItem(Item item)
    {

    }
}
