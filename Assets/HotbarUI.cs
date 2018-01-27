using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public GameObject selector;
    private List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    private bool invWasPressed;
    private int selectedHotbarSlot = 0;
    private bool activated = true;

    private void Awake()
    {
        itemSlots.AddRange(GetComponentsInChildren<ItemSlotUI>());
    }

    private void Start()
    {
        SelectHotbar(0);
    }

    private void Update()
    {
        if(activated)
        {
            float horizontalChange = Input.GetAxisRaw("InvHorizontal");

            if (!invWasPressed)
            {
                selectedHotbarSlot += Mathf.FloorToInt(horizontalChange);
            }

            invWasPressed = (horizontalChange != 0f) ? true : false;

            selectedHotbarSlot = InventoryUI.WrapAround(selectedHotbarSlot, 0, itemSlots.Count - 1);

            SelectHotbar(selectedHotbarSlot);
        }
    }

    public void SelectHotbar(int id)
    {
        selectedHotbarSlot = Mathf.Clamp(id, 0, itemSlots.Count);
        selector.transform.SetParent(itemSlots[selectedHotbarSlot].transform);
        selector.transform.localPosition = Vector3.zero;
    }

    public int GetHotbarSize()
    {
        return itemSlots.Count;
    }

    public void UpdateUi(List<ItemSlot> hotbar)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].SetSlot(hotbar[i]);
        }
    }

    public List<ItemSlotUI> GetSlotUi()
    {
        return itemSlots;
    }

    public void Deactivate()
    {
        activated = false;
        selector.SetActive(false);
    }

    public void Activate()
    {
        activated = true;
        selector.SetActive(true);
    }

    public int GetSelectedHotbar()
    {
        return selectedHotbarSlot;
    }
}
