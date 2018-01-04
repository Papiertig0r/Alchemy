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
            itemSlotUIs[i].UpdateItem(inventory[i]);
        }

        for(; i < itemSlotUIs.Count; i++)
        {
            itemSlotUIs[i].RemoveItem();
        }
    }

    public void Update()
    {
        SelectItemSlot();
    }

    private void SelectItemSlot()
    {
        int x = 0;
        int y = 0;
        if (Input.GetButtonDown("Horizontal"))
        {
            x = (int)Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        if (Input.GetButtonDown("Vertical"))
        {
            y = (int)-Mathf.Sign(Input.GetAxis("Vertical"));
        }

        if (x != 0 || y != 0)
        {
            if (ItemSlotUI.selectedSlot == null)
            {
                ItemSlotUI.MarkSlot(itemSlotUIs[0]);
            }
            else
            {
                int currentSlotIndex = itemSlotUIs.FindIndex(slot => slot == ItemSlotUI.selectedSlot);
                Vector2 slotPosition = new Vector2(currentSlotIndex % slotsPerLine, currentSlotIndex / slotsPerLine);
                slotPosition.x = Mathf.Clamp(slotPosition.x + x, 0, slotsPerLine - 1);
                slotPosition.y = Mathf.Clamp(slotPosition.y + y, 0, linesOfSlots - 1);

                currentSlotIndex = (int)slotPosition.x + (int)slotPosition.y * slotsPerLine;
                ItemSlotUI.MarkSlot(itemSlotUIs[currentSlotIndex]);
            }
        }
    }
}
