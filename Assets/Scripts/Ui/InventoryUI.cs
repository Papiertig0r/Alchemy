using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotSelector;
    public GameObject inventorySelector;
    public HotbarUI hotbar;

    public ItemInfoUI itemInfoUi;


    private List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();
    private List<ItemSlotUI> allInventorySlots = new List<ItemSlotUI>();
    private List<ItemSlot> inventory = new List<ItemSlot>();

    private int selectedInventorySlot = 0;
    private bool released = true;
    private IShowable currentExtendenInfo;

    private void OnEnable()
    {
        hotbar.Deactivate();
        if(selectedInventorySlot < hotbar.GetHotbarSize())
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
        if(selectedInventorySlot < hotbar.GetHotbarSize())
        {
            hotbar.SelectHotbar(selectedInventorySlot);
        }
        hotbar.Activate();
    }

    private void Update()
    {
        if(!StateController.IsInState(State.INVENTORY))
        {
            return;
        }

        float horizontalChange = Input.GetAxisRaw("InvHorizontal");
        float verticalChange = Input.GetAxisRaw("InvVertical");

        // works only if inventory UI is same size as hotbar
        int x = selectedInventorySlot % hotbar.GetHotbarSize();
        int y = selectedInventorySlot / hotbar.GetHotbarSize();
        if ((horizontalChange != 0f || verticalChange != 0f) && released)
        {
            released = false;
            x += Mathf.FloorToInt(horizontalChange);
            y += Mathf.FloorToInt(verticalChange);
            x = WrapAround(x, 0, hotbar.GetHotbarSize() - 1);
            y = WrapAround(y, 0, allInventorySlots.Count / hotbar.GetHotbarSize() - 1);

            selectedInventorySlot = x + y * hotbar.GetHotbarSize();
            SelectSlot(selectedInventorySlot);
            HandleInfoUi();
        }
        else if(horizontalChange == 0f && verticalChange == 0f && ! released)
        {
            released = true;
        }

        Item item = inventory[selectedInventorySlot].item;
        if (Input.GetButtonDown("Action") && item != null)
        {
            UIManager.inventorySubmenuUi.SetUp(item);
        }
    }

    public int GetInventorySize()
    {
        return inventorySlots.Count;
    }

    public void InitInventoryUI(List<ItemSlotUI> hotbarSlots)
    {
        inventorySlots.AddRange(GetComponentsInChildren<ItemSlotUI>());

        allInventorySlots.AddRange(hotbar.GetSlotUi());
        allInventorySlots.AddRange(inventorySlots);
    }

    public void UpdateUi(List<ItemSlot> inventory)
    {
        this.inventory = inventory;
        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            allInventorySlots[i].SetSlot(inventory[i]);
        }
        if (gameObject.activeSelf)
        {
            HandleInfoUi();
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
        {
            StateController.Transition(State.INVENTORY);
        }
        else
        {
            StateController.Transition(State.WORLD);
        }
    }

    public void SelectSlot(int id)
    {
        selectedInventorySlot = Mathf.Clamp(id, 0, allInventorySlots.Count);
        inventorySlotSelector.transform.SetParent(allInventorySlots[selectedInventorySlot].transform);
        inventorySlotSelector.transform.localPosition = Vector3.zero;
    }

    public static int WrapAround(int value, int min, int max)
    {
        if(value < min)
        {
            value = max;
        }
        if(value > max)
        {
            value = min;
        }
        return value;
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
        if(currentExtendenInfo != null)
        {
            currentExtendenInfo.DeactivateExtendedInfo();
        }
        currentExtendenInfo = showable;
        if (showable != null)
        {
            showable.ShowExtendedInfo();
        }
    }

    public int GetSelectedSlot()
    {
        return selectedInventorySlot;
    }
}
