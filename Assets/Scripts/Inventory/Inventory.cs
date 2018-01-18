using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int hotbarSize = 7;
    public int slotCount = 6;
    public int slotSize = 20;

    [SerializeField]
    private int selectedHotbarSlot = 0;
    [SerializeField]
    public List<ItemSlot> itemSlots;

    private void Awake()
    {
        InitItemSlots();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetButtonDown("SwitchLeft"))
        {
            selectedHotbarSlot--;
        }

        if (Input.GetButtonDown("SwitchRight"))
        {
            selectedHotbarSlot++;
        }

        if(selectedHotbarSlot < 0)
        {
            selectedHotbarSlot = hotbarSize - 1;
        }
        else if(selectedHotbarSlot == hotbarSize)
        {
            selectedHotbarSlot = 0;
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        List<ItemSlot> slotsWithItem = FindSlotsWithItem(itemToAdd);

        for(int i = 0; i < slotsWithItem.Count; i++)
        {
            if(slotsWithItem[i].quantity < itemToAdd.stackSize)
            {
                slotsWithItem[i].quantity++;
                Destroy(itemToAdd.gameObject);
                return true;
            }
        }

        int index = FindFirstEmptySlot();
        if(index == -1)
        {
            return false;
        }
        itemSlots[index].item = itemToAdd;
        itemSlots[index].quantity++;

        return true;
    }

    public List<ItemSlot> FindSlotsWithItem(Item item)
    {
        List<ItemSlot> foundSlots = itemSlots.FindAll(slots => slots.item != null && slots.item.name == item.name);
        return foundSlots;
    }

    public int FindFirstEmptySlot()
    {
        return itemSlots.FindIndex(slot => slot.item == null);
    }

    public int FindSlotsWithCapacity(List<ItemSlot> slots)
    {
        return itemSlots.FindIndex(slot => slot.quantity < slot.item.stackSize);
    }

    public void RemoveItem(int fromSlot)
    {
        RemoveItemFromSlot(itemSlots[fromSlot]);
    }

    public void RemoveItemFromSlot(ItemSlot slot)
    {
        slot.quantity--;
        if (slot.quantity == 0)
        {
            DestroyObject(slot.item.gameObject);
            slot.item = null;
        }
    }

    public Item GetHotbarItem()
    {
        Item item = itemSlots[selectedHotbarSlot].item;
        return item;
    }

    public Item InstantiateItem(Item item)
    {
        if (item != null)
        {
            item = Instantiate<Item>(item, this.transform);
            item.gameObject.name = item.name;
        }
        return item;
    }

    public void RemoveItemForRangedAttack()
    {
        RemoveItem(selectedHotbarSlot);
    }

    private void InitItemSlots()
    {
        itemSlots = new List<ItemSlot>(hotbarSize + slotCount);
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            itemSlots.Add(new ItemSlot());
        }
    }
}
