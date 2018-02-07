using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public static PlayerInventory instance = null;

    public int hotbarSize = 7;

    public HotbarUI hotbarUi;

    public Material defaultMaterial;
    public Material greyscaleMaterial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(this.gameObject);
        }

        InitItemSlots(hotbarSize + numberOfSlots);
    }

    protected override void Start()
    {
        hotbarSize = hotbarUi.GetHotbarSize();
        inventoryUi.InitInventoryUI(hotbarUi.GetSlotUi());
        numberOfSlots = inventoryUi.GetInventorySize();

        InitItemSlots(hotbarSize + numberOfSlots);
    }

    public void ConsumeHotbarItem()
    {
        Item item = itemSlots[hotbarUi.GetSelectedHotbar()].item;
        if (item != null)
        {
            IConsumable consumable = item.GetComponent<IConsumable>();
            if (consumable != null)
            {
                consumable = InstantiateItem(item).GetComponent<IConsumable>();
                consumable.Consume(PlayerController.player);
                RemoveItemFromSlot(itemSlots[hotbarUi.GetSelectedHotbar()]);
            }
        }
    }

    public void RemoveSelectedHotbarItem()
    {
        RemoveItem(hotbarUi.GetSelectedHotbar());
    }

    public Item GetHotbarItem()
    {
        Item item = itemSlots[hotbarUi.GetSelectedHotbar()].item;
        return item;
    }

    public void RemoveItemForRangedAttack()
    {
        RemoveItem(hotbarUi.GetSelectedHotbar());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUi.Toggle();
        }

        if (Input.GetButtonDown("Dodge/Abort") && StateController.IsInState(State.INVENTORY))
        {
            inventoryUi.Toggle();
        }
    }
}
