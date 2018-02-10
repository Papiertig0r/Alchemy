using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int numberOfSlots = 6;
    public int slotSize = 20;

    [SerializeField]
    private InventoryUI inventoryUi;

    [SerializeField]
    protected List<ItemSlot> itemSlots;

    protected virtual void Start()
    {
        InitItemSlots(numberOfSlots);
    }

    public void SetUi(InventoryUI inventoryUi)
    {
        this.inventoryUi = inventoryUi;
    }

    public bool AddItem(Item itemToAdd)
    {
        Debug.Log(itemToAdd);
        List<ItemSlot> slotsWithItem = FindSlotsWithItem(itemToAdd);

        for(int i = 0; i < slotsWithItem.Count; i++)
        {
            if(slotsWithItem[i].quantity < itemToAdd.stackSize)
            {
                slotsWithItem[i].quantity++;
                Destroy(itemToAdd.gameObject);
                UpdateUi();
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
        itemToAdd.transform.SetParent(transform);
        itemToAdd.gameObject.SetActive(false);
        UpdateUi();
        return true;
    }

    public List<ItemSlot> FindSlotsWithItem(Item item)
    {
        //List<ItemSlot> foundSlots = itemSlots.FindAll(slots => slots.item != null && slots.item.name == item.name);
        List<ItemSlot> foundSlots = itemSlots.FindAll(slots => slots.item != null && slots.item.name == item.name &&slots.item.Compare(item.GetComponent<IComparable>()));
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

        UpdateUi();
    }

    public void InitItemSlots(int numberOfSlots = 0)
    {
        if(numberOfSlots == 0)
        {
            numberOfSlots = this.numberOfSlots;
        }
        itemSlots = new List<ItemSlot>(numberOfSlots);
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            itemSlots.Add(new ItemSlot());
        }
        UpdateUi();
    }

    public void UpdateUi(ItemSlot itemSlot = null)
    {
        if(inventoryUi != null)
        {
            inventoryUi.UpdateUi(itemSlots, itemSlot);
        }
    }

    public Item InstantiateItem(Item item)
    {
        return InstantiateItem(item, this.transform);
    }

    public static Item InstantiateItem(Item item, Transform parent)
    {
        if (item != null)
        {
            item = Instantiate<Item>(item, parent);
            item.gameObject.name = item.name;
        }
        return item;
    }
}
