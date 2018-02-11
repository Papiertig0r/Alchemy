using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public GameObject selector;
    private List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    private int selectedHotbarSlot = 0;

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
        if(StateController.IsInState(State.WORLD))
        {
            int change = Input.GetButtonDown("SwitchLeft") ? -1 : 0;
            change += Input.GetButtonDown("SwitchRight") ? 1 : 0;

            if(change != 0)
            {
                selectedHotbarSlot += change;
                selectedHotbarSlot = InventoryUI.WrapAround(selectedHotbarSlot, 0, itemSlots.Count - 1);

                SelectHotbar(selectedHotbarSlot);
            }
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

    public List<ItemSlotUI> GetSlotUi()
    {
        return itemSlots;
    }

    public void Deactivate()
    {
        selector.SetActive(false);
    }

    public void Activate()
    {
        selector.SetActive(true);
    }

    public int GetSelectedHotbar()
    {
        return selectedHotbarSlot;
    }
}
