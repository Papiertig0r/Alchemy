using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : InventoryUI
{
    public GameObject inventorySlotSelector;
    public GameObject inventorySelector;
    public HotbarUI hotbar;

    public ItemInfoUI itemInfoUi;

    private List<ItemSlotUI> allInventorySlots = new List<ItemSlotUI>();
    private List<ItemSlot> inventory = new List<ItemSlot>();

    //private bool released = true;
    private IShowable currentExtendedInfo;

    private ItemSlot mixingItem = null;

    private void OnEnable()
    {
        hotbar.Deactivate();
        if (selectedInventorySlot < hotbar.GetHotbarSize())
        {
            SelectSlot(hotbar.GetSelectedHotbar());
        }
        inventorySlotSelector.SetActive(true);
        HandleInfoUi();
    }

    private void OnDisable()
    {
        itemInfoUi.Deactivate();
        inventorySlotSelector.SetActive(false);
        if (selectedInventorySlot < hotbar.GetHotbarSize())
        {
            hotbar.SelectHotbar(selectedInventorySlot);
        }
        hotbar.Activate();
    }

    private void Update()
    {
        if (!(
            StateController.IsInState(State.INVENTORY) ||
            StateController.IsInState(State.MIXING)
            ))
        {
            return;
        }

        HandleNavigation();

        Item item = inventory[selectedInventorySlot].item;
        if (Input.GetButtonDown("Action") && item != null)
        {
            switch (StateController.GetState())
            {
                case State.INVENTORY:
                    UIManager.inventorySubmenuUi.SetUp(item, allInventorySlots[selectedInventorySlot]);
                    break;

                case State.MIXING:
                    TryToMix();
                    break;
            }
        }

        if (Input.GetButtonDown("Dodge/Abort") && StateController.IsInState(State.MIXING))
        {
            EndMixing();
        }
    }

    protected override void OnPress(Vector2 input)
    {
        int x = selectedInventorySlot % hotbar.GetHotbarSize();
        int y = selectedInventorySlot / hotbar.GetHotbarSize();

        x += Mathf.FloorToInt(input.x);
        y += Mathf.FloorToInt(input.y);
        x = InventoryUI.WrapAround(x, 0, hotbar.GetHotbarSize() - 1);
        y = InventoryUI.WrapAround(y, 0, allInventorySlots.Count / hotbar.GetHotbarSize() - 1);

        selectedInventorySlot = x + y * hotbar.GetHotbarSize();
        SelectSlot(selectedInventorySlot);
        HandleInfoUi();
    }

    public override void UpdateUi(List<ItemSlot> inventory, ItemSlot slot)
    {
        this.inventory = inventory;
        UpdateUi(slot);
    }

    public void UpdateUi(ItemSlot slot = null)
    {
        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            bool isActive = !CheckForCompatability(slot, i);
            allInventorySlots[i].SetSlot(inventory[i], isActive);
        }
        if (gameObject.activeSelf)
        {
            HandleInfoUi();
        }
    }

    private void TryToMix()
    {
        Debug.Log("mixing...");
        ItemSlot slot = inventory[selectedInventorySlot];
        if (slot == null)
        {
            return;
        }

        Debug.Log("there is a slot...");
        Item item = slot.item;
        if (item == null || slot.quantity <= 1)
        {
            return;
        }
        Debug.Log("there is an item...");

        Ingredient ingredient = item.GetComponent<Ingredient>();
        if (ingredient == null)
        {
            return;
        }
        Debug.Log("there is an ingredient...");

        Item otherItem = mixingItem.item;
        Ingredient otherIngredient = otherItem.GetComponent<Ingredient>();
        if (ingredient.IsCompatible(otherIngredient))
        {
            Debug.Log("Mixing");
            PlayerInventory inv = PlayerInventory.instance;
            Item mixedItem = inv.InstantiateItem(item);
            ingredient = mixedItem.GetComponent<Ingredient>();
            ingredient.Mix(otherIngredient);

            inv.RemoveItemFromSlot(inventory[selectedInventorySlot]);
            inv.RemoveItemFromSlot(mixingItem);
            inv.AddItem(mixedItem);
            EndMixing();
        }
    }

    public void MarkMixingItem()
    {
        mixingItem = inventory[GetSelectedSlotNumber()];
    }

    public void StartMixing()
    {
        StateController.Transition(State.MIXING);
        UpdateUi(mixingItem);
    }

    public void EndMixing()
    {
        mixingItem = null;
        UpdateUi();
        StateController.Transition(State.INVENTORY);
    }

    public void InitInventoryUI(List<ItemSlotUI> hotbarSlots)
    {
        inventorySlots.AddRange(GetComponentsInChildren<ItemSlotUI>());

        allInventorySlots.AddRange(hotbar.GetSlotUi());
        allInventorySlots.AddRange(inventorySlots);
    }

    private bool CheckForCompatability(ItemSlot slot, int currentIndex)
    {
        if (slot == null)
        {
            return false;
        }
        if (slot.quantity < 2)
        {
            Debug.Log(slot.item.name + " less than 2");
            return true;
        }
        bool isActive = true;

        Ingredient ingredient = null;
        if (slot != null)
        {
            if (slot == inventory[currentIndex] && inventory[currentIndex].quantity <= 1)
            {
                isActive = false;
            }
        }

        if (inventory[currentIndex].item != null)
        {
            ingredient = inventory[currentIndex].item.GetComponent<Ingredient>();
        }

        if (ingredient == null)
        {
            isActive = true;
        }
        else
        {
            isActive = !ingredient.IsCompatible(slot.item.GetComponent<Ingredient>());
        }

        return isActive;
    }

    public override void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            StateController.Transition(State.INVENTORY);
        }
        else
        {
            UIManager.inventorySubmenuUi.Leave();
            StateController.Transition(State.WORLD);
        }
    }

    public void SelectSlot(int id)
    {
        selectedInventorySlot = Mathf.Clamp(id, 0, allInventorySlots.Count);
        inventorySlotSelector.transform.SetParent(allInventorySlots[selectedInventorySlot].transform);
        inventorySlotSelector.transform.localPosition = Vector3.zero;
    }

    private void HandleInfoUi()
    {
        Item item = inventory[selectedInventorySlot].item;
        if (item == null)
        {
            itemInfoUi.Deactivate();
        }
        else
        {
            itemInfoUi.SetItemName(item.name);
            itemInfoUi.Activate();
            IShowable showable = item.GetComponent<IShowable>();
            HandleExtendedInfoUi(showable);
        }
    }

    private void HandleExtendedInfoUi(IShowable showable)
    {
        if (currentExtendedInfo != null)
        {
            currentExtendedInfo.DeactivateExtendedInfo();
        }
        currentExtendedInfo = showable;
        if (showable != null)
        {
            showable.ShowExtendedInfo();
        }
    }

    public int GetSelectedSlotNumber()
    {
        return selectedInventorySlot;
    }

    public ItemSlot GetSelectedSlot()
    {
        if (StateController.IsInState(State.WORLD))
        {
            return inventory[hotbar.GetSelectedHotbar()];
        }
        else
        {
            return inventory[selectedInventorySlot];
        }
    }
}
