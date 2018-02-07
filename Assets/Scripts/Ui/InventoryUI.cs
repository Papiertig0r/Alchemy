using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    protected List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();

    protected int selectedInventorySlot = 0;

    private bool released = true;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
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
