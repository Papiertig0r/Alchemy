using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image image;
    public Text text;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        image.preserveAspect = true;
        image.gameObject.SetActive(sprite != null);
        text.gameObject.SetActive(sprite != null);
    }

    public void SetSlot(ItemSlot slot, bool isActive = true)
    {
        if(slot.item != null)
        {
            text.text = slot.quantity.ToString();
            SetSprite(slot.item.GetComponent<SpriteRenderer>().sprite);
            PlayerInventory inv = PlayerInventory.instance;
            image.material = isActive ? inv.defaultMaterial : inv.greyscaleMaterial;
        }
        else
        {
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }
}
