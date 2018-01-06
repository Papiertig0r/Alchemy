using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<ItemSlotUI> itemSlotUIs = new List<ItemSlotUI>();

    public int slotsPerLine;
    public int linesOfSlots;

    public void UpdateInventoryUI(List<ItemSlot> inventory)
    {
        int i;

        for (i = 0; i < inventory.Count; i++)
        {
            itemSlotUIs[i].AddItemToSlot(inventory[i]);
        }

        for(; i < itemSlotUIs.Count; i++)
        {
            itemSlotUIs[i].RemoveItemFromSlot();
        }
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public bool active
    {
        get
        {
            return gameObject.activeSelf;
        }
    }
}
