using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public GameObject selector;
    private List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();

    private void Awake()
    {
        itemSlots.AddRange(GetComponentsInChildren<ItemSlotUI>());
    }

    private void Start()
    {
        SelectHotbar(0);
    }

    public void SelectHotbar(int id)
    {
        id = Mathf.Clamp(id, 0, itemSlots.Count);
        selector.transform.SetParent(itemSlots[id].transform);
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
}
