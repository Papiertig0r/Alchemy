using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static ItemSlotUI selectedSlot = null;

    public Text quantityText;
    public Image itemImage;
    public ItemSlot itemSlot = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MarkSlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void AddItemSlot(ItemSlot itemSlot)
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
            quantityText.text = itemSlot.quantity.ToString();
            quantityText.gameObject.SetActive(true);
            itemImage.sprite = itemSlot.item.sprite;
            itemImage.preserveAspect = true;
            itemImage.gameObject.SetActive(true);
        }
    }

    public void RemoveItemSlot()
    {
        this.itemSlot = null;
        UpdateItemSlotUI();
    }

    public static void MarkSlot(ItemSlotUI slot)
    {
        //! \todo better selection method than just color (colored frame!)
        if(selectedSlot != null)
        {
            selectedSlot.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.4f);
        }
        selectedSlot = slot;
        selectedSlot.GetComponent<Image>().color = Color.red;
    }
}
