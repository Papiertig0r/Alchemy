using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public int slotCount = 6;
    public int slotSize = 20;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public bool AddItem(Item itemToAdd)
    {
        ItemSlot slot = itemSlots.Find(itemSlot => itemSlot.item.name == itemToAdd.name);

        if(slot == null)
        {
            itemSlots.Add(new ItemSlot(itemToAdd));
        }
        else
        {
            slot.quantity++;
        }

        return true;
    }

    public void RemoveItem(Item item)
    {

    }
}
