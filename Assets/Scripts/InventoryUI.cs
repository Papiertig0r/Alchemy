using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<ItemSlotUI> itemSlotUIs = new List<ItemSlotUI>();

    public void UpdateInventoryUI(List<ItemSlot> inventory)
    {

        for(int i = 0; i < inventory.Count; i++)
        {
            itemSlotUIs[i].text.text = inventory[i].quantity.ToString();
            itemSlotUIs[i].text.gameObject.SetActive(true);
            itemSlotUIs[i].image.sprite = inventory[i].item.sprite;
            itemSlotUIs[i].image.SetNativeSize();
            itemSlotUIs[i].image.gameObject.SetActive(true);
        }
    }
}
