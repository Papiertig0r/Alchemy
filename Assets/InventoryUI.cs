using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotSelector;
    public GameObject inventorySelector;
    public HotbarUI hotbar;
    private List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();
    private List<ItemSlotUI> allInventorySlots = new List<ItemSlotUI>();

    private int selectedInventorySlot = 0;
    private bool invWasPressed = false;

    private void OnEnable()
    {
        hotbar.Deactivate();
        if(selectedInventorySlot < hotbar.GetHotbarSize())
        {
            SelectSlot(hotbar.GetSelectedHotbar());
        }
        inventorySlotSelector.SetActive(true);
    }

    private void OnDisable()
    {
        inventorySlotSelector.SetActive(false);
        if(selectedInventorySlot < hotbar.GetHotbarSize())
        {
            hotbar.SelectHotbar(selectedInventorySlot);
        }
        hotbar.Activate();
    }

    private void Update()
    {
        float horizontalChange = Input.GetAxisRaw("InvHorizontal");
        float verticalChange = Input.GetAxisRaw("InvVertical");

        // works only if inventory UI is same size as hotbar
        int x = selectedInventorySlot % hotbar.GetHotbarSize();
        int y = selectedInventorySlot / hotbar.GetHotbarSize();
        if (!invWasPressed)
        {
            x += Mathf.FloorToInt(horizontalChange);
            y += Mathf.FloorToInt(verticalChange);
            x = WrapAround(x, 0, hotbar.GetHotbarSize() - 1);
            y = WrapAround(y, 0, allInventorySlots.Count / hotbar.GetHotbarSize() - 1);
        }

        invWasPressed = (horizontalChange != 0f) ? true : false;
        invWasPressed |= (verticalChange != 0f) ? true : false;

        selectedInventorySlot = x + y * hotbar.GetHotbarSize();
        SelectSlot(selectedInventorySlot);
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
        for (int i = 0; i < allInventorySlots.Count; i++)
        {
            allInventorySlots[i].SetSlot(inventory[i]);
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SelectSlot(int id)
    {
        id = Mathf.Clamp(id, 0, allInventorySlots.Count);
        inventorySlotSelector.transform.SetParent(allInventorySlots[id].transform);
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
}
