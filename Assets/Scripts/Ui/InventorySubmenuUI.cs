using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySubmenuUI : MonoBehaviour
{
    public Button consume;
    public Button mix;
    public Button equip;
    public Button discard;

    Item item;

    public void SetUp(Item item)
    {
        IConsumable consumable = item.GetComponent<IConsumable>();
        if(consumable != null)
        {
            consume.gameObject.SetActive(true);
        }

        Ingredient ingredient = item.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            mix.gameObject.SetActive(true);
        }

        IEquippable equipment = item.GetComponent<IEquippable>();
        if(equipment != null)
        {
            equip.gameObject.SetActive(true);
        }
    }

    public void Leave()
    {
        item = null;
        consume.gameObject.SetActive(false);
        mix.gameObject.SetActive(false);
        equip.gameObject.SetActive(false);
    }

    public void Consume()
    {
        IConsumable consumable = item.GetComponent<IConsumable>();
        consumable.Consume(PlayerController.player);
    }

    public void Mix()
    {
        //Get all ingredients in inventory
        //Check if ingredient is mixable
        //Disable menu
        //Grey all non selectable
        //Abort with "Dodge/Abort"
        //If other ingredient is selected, mix ingredients
    }

    public void Equip()
    {
        //Equip item
    }

    public void Discard()
    {
        //Discard item
    }
}
