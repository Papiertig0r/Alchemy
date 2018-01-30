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
    private List<Button> activeButtons = new List<Button>();
    private int selectedButton = 0;
    private bool released = true;

    public void SetUp(Item item)
    {
        selectedButton = 0;
        activeButtons.Clear();
        IConsumable consumable = item.GetComponent<IConsumable>();
        if(consumable != null)
        {
            activeButtons.Add(consume);
            consume.gameObject.SetActive(true);
        }

        Ingredient ingredient = item.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            activeButtons.Add(mix);
            mix.gameObject.SetActive(true);
        }

        IEquippable equipment = item.GetComponent<IEquippable>();
        if(equipment != null)
        {
            activeButtons.Add(equip);
            equip.gameObject.SetActive(true);
        }

        activeButtons.Add(discard);
        activeButtons[selectedButton].Select();
    }

    public void Leave()
    {
        item = null;
        consume.gameObject.SetActive(false);
        mix.gameObject.SetActive(false);
        equip.gameObject.SetActive(false);

        StateController.Transition(State.INVENTORY);
    }

    private void Update()
    {
        if (!StateController.IsInState(State.INVENTORY_SUBMENU))
        {
            return;
        }

        float change = Input.GetAxis("InvVertical");

        if (change != 0f && released)
        {
            selectedButton -= Mathf.FloorToInt(change);
            selectedButton = InventoryUI.WrapAround(selectedButton, 0, activeButtons.Count - 1);
            activeButtons[selectedButton].Select();
            released = false;
        }
        else if (change == 0f && !released)
        {
            released = true;
        }

        if(Input.GetButtonDown("Action"))
        {
            activeButtons[selectedButton].onClick.Invoke();
        }

        if(Input.GetButtonDown("Dodge/Abort"))
        {
            Leave();
        }
    }

    public void Consume()
    {
        //IConsumable consumable = item.GetComponent<IConsumable>();
        //consumable.Consume(PlayerController.player);
        Debug.Log("Clicked Consume");
    }

    public void Mix()
    {
        //Get all ingredients in inventory
        //Check if ingredient is mixable
        //Disable menu
        //Grey all non selectable
        //Abort with "Dodge/Abort"
        //If other ingredient is selected, mix ingredients
        Debug.Log("Clicked Mix");
    }

    public void Equip()
    {
        //Equip item
        Debug.Log("Clicked Equip");
    }

    public void Discard()
    {
        //Discard item
        Debug.Log("Clicked discard");
    }
}
