using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySelector;

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

    public void Select(bool showSelector = false)
    {
        inventorySelector.SetActive(showSelector);
        isSelected = true;
    }

    public void Unselect()
    {
        inventorySelector.SetActive(false);
        isSelected = false;
    }

    protected Vector2 HandleNavigation()
    {
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
