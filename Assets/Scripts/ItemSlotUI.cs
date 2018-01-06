using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public static ItemSlotUI selectedSlot = null;

    public Text quantityText;
    public Image itemImage;

    public bool showQuantity;
    public int capacity;

    private ItemSlot itemSlot = null;

    public void AddItemToSlot(ItemSlot itemSlot)
    {
        this.itemSlot = itemSlot;
        UpdateItemSlotUI();
    }

    public void UpdateItemSlotUI()
    {
        if(itemSlot == null)
        {
            quantityText.text = "";
            quantityText.gameObject.SetActive(false);
            itemImage.sprite = null;
            itemImage.gameObject.SetActive(false);
        }
        else
        {
            if(showQuantity)
            {
                quantityText.text = itemSlot.quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
            itemImage.sprite = itemSlot.item.sprite;
            itemImage.preserveAspect = true;
            itemImage.gameObject.SetActive(true);
        }
    }

    public void RemoveItemFromSlot()
    {
        this.itemSlot = null;
        UpdateItemSlotUI();
    }

    public void ApplyItem()
    {
        if (itemSlot != null)
        {
            if(itemSlot.item.Apply())
            {
            }
        }
    }
}
