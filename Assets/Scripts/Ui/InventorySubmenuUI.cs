using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySubmenuUI : MonoBehaviour
{
    public Button consume;
    public Button mix;
    public Button apply;
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
        this.item = item;

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

        IApplyable applyable = item.GetComponent<IApplyable>();
        if(applyable != null)
        {
            activeButtons.Add(apply);
            apply.gameObject.SetActive(true);
        }

        IEquippable equipment = item.GetComponent<IEquippable>();
        if(equipment != null)
        {
            activeButtons.Add(equip);
            equip.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
        activeButtons.Add(discard);
        activeButtons[selectedButton].Select();

        StateController.Transition(State.INVENTORY_SUBMENU);
    }

    public void Leave()
    {
        if(!StateController.IsInState(State.INVENTORY_SUBMENU))
        {
            return;
        }
        item = null;

        EventSystem.current.SetSelectedGameObject(null);

        consume.gameObject.SetActive(false);
        mix.gameObject.SetActive(false);
        equip.gameObject.SetActive(false);

        gameObject.SetActive(false);
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

        if(Input.GetButtonDown("Dodge/Abort") || Input.GetButtonDown("Inventory"))
        {
            Leave();
            StateController.Transition(State.INVENTORY);
        }
    }

    public void Consume()
    {
        Inventory inv = Inventory.instance;
        Item item = inv.InstantiateItem(this.item);
        IConsumable consumable = item.GetComponent<IConsumable>();
        consumable.Consume(PlayerController.player);
        inv.RemoveItem(inv.inventoryUi.GetSelectedSlot());
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
        Leave();
        Inventory.instance.StartMixing();
    }

    public void Apply()
    {
        // Apply item (e.g. poison to a weapon)
        Debug.Log("Clicked Apply");
    }

    public void Equip()
    {
        //Equip item
        Debug.Log("Clicked Equip");
    }

    public void Discard()
    {
        //! \todo detect long presses for stackwise discarding
        Inventory.instance.RemoveItem(Inventory.instance.inventoryUi.GetSelectedSlotNumber());
        Debug.Log(Inventory.instance.inventoryUi.GetSelectedSlot());
        if (Inventory.instance.inventoryUi.GetSelectedSlot().item == null)
        {
            UIManager.inventorySubmenuUi.Leave();
            StateController.Transition(State.INVENTORY);
        }
    }
}
