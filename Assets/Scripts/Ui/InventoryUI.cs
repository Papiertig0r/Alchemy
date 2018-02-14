﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySelector;
    public GameObject inventorySlotSelector;

    [SerializeField]
    protected List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();

    protected int selectedInventorySlot = 0;

    private bool released = true;
    private bool isSelected = false;

    public void Activate()
    {
        gameObject.SetActive(true);
        UIManager.instance.Register(this);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        UIManager.instance.Unregister(this);
    }

    public virtual void Toggle()
    {

    }

    public virtual void SetUp(int numberOfSlots)
    {

    }

    public virtual void UpdateUi(List<ItemSlot> inventory, ItemSlot slot)
    {

    }

    public int GetInventorySize()
    {
        return inventorySlots.Count;
    }

    public virtual void Select(bool showSelector = false)
    {
        inventorySelector.SetActive(showSelector);
        isSelected = true;
    }

    public void Unselect()
    {
        inventorySelector.SetActive(false);
        isSelected = false;
    }

    public virtual void SelectSlot(int id, List<ItemSlotUI> slots)
    {
        selectedInventorySlot = Mathf.Clamp(id, 0, slots.Count);
        inventorySlotSelector.transform.SetParent(slots[selectedInventorySlot].transform);
        inventorySlotSelector.transform.localPosition = Vector3.zero;
    }

    protected virtual void Update()
    {
        HandleNavigation();
    }

    protected Vector2 HandleNavigation()
    {
        if(!isSelected)
        {
            return new Vector2(0f, 0f);
        }
        float horizontalChange = Input.GetAxisRaw("InvHorizontal");
        float verticalChange = Input.GetAxisRaw("InvVertical");
        Vector2 change = new Vector2(horizontalChange, verticalChange);
        if ((horizontalChange != 0f || verticalChange != 0f) && released)
        {
            released = false;
            OnPress(change);
        }
        else if (horizontalChange == 0f && verticalChange == 0f && !released)
        {
            released = true;
            OnRelease();
        }

        return change;
    }

    protected virtual void OnPress(Vector2 input)
    {
    }

    protected virtual void HandleSelection(Vector2 input, int columns, int rows)
    {
        int x = selectedInventorySlot % columns;
        int y = selectedInventorySlot / columns;

        x += Mathf.FloorToInt(input.x);
        y += Mathf.FloorToInt(input.y);
        x = InventoryUI.WrapAround(x, 0, columns - 1);
        y = InventoryUI.WrapAround(y, 0, rows - 1);

        selectedInventorySlot = x + y * columns;
    }

    protected virtual void OnRelease()
    {

    }

    public static int WrapAround(int value, int min, int max)
    {
        if (value < min)
        {
            value = max;
        }
        if (value > max)
        {
            value = min;
        }
        return value;
    }
}
