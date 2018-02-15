using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : InventoryUI
{
    public ItemSlotUI prefab;
    public Transform panel;

    public RectTransform actualBorders;

    private bool needsResizing = false;

    public void SetUp(int numberOfSlots)
    {
        if (numberOfSlots > inventorySlots.Count)
        {
            int diff = numberOfSlots - inventorySlots.Count;
            for(; diff > 0; diff--)
            {
                inventorySlots.Add(Instantiate(prefab, panel));
            }
        }

        for(int i = 0; i < inventorySlots.Count; i++)
        {
            bool isActive = i < numberOfSlots;
            inventorySlots[i].gameObject.SetActive(isActive);
        }

        columns = Mathf.Clamp(numberOfSlots / 2, numberOfSlots % 4, 6);
        rows = numberOfSlots / columns;
        panel.GetComponent<GridLayoutGroup>().constraintCount = columns;

        SelectSlot(0);
    }

    public override void UpdateUi(List<ItemSlot> inventory, ItemSlot slot)
    {
        for (int i = 0; i < inventorySlots.Count && i < inventory.Count; i++)
        {
            bool isActive = true;
            inventorySlots[i].SetSlot(inventory[i], isActive);
        }
    }

    public override void Select(bool showSelector = false)
    {
        base.Select(showSelector);
        needsResizing = true;
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetButtonDown("Dodge/Abort"))
        {
            Deactivate();
        }
    }

    private void LateUpdate()
    {
        if(needsResizing)
        {
            RectTransform inv = inventorySelector.GetComponent<RectTransform>();
            inv.sizeDelta = actualBorders.sizeDelta;
            needsResizing = false;
        }
    }
}
