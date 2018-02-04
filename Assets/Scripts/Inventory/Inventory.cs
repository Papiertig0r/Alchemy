using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;

    public int hotbarSize = 7;
    public int slotCount = 6;
    public int slotSize = 20;

    public HotbarUI hotbarUi;
    public InventoryUI inventoryUi;

    public Material defaultMaterial;
    public Material greyscaleMaterial;

    [SerializeField]
    private List<ItemSlot> itemSlots;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if(instance != this)
        {
            Destroy(this.gameObject);
        }

        InitItemSlots();
    }

    private void Start()
    {
        hotbarSize = hotbarUi.GetHotbarSize();
        inventoryUi.InitInventoryUI(hotbarUi.GetSlotUi());
        slotCount = inventoryUi.GetInventorySize();
        InitItemSlots();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUi.Toggle();
        }

        if(Input.GetButtonDown("Dodge/Abort"))
        {
            switch(StateController.GetState())
            {
                case State.INVENTORY:
                    inventoryUi.Toggle();
                    break;
                case State.MIXING:
                    EndMixing();
                    break;
            }
        }
    }

    public void StartMixing()
    {
        StateController.Transition(State.MIXING);
        // Search through inventory if items are mixable
        // Grey out items that are not
        UpdateUi(itemSlots[inventoryUi.GetSelectedSlot()]);
    }

    public void EndMixing()
    {
        //restore all items to original color

        UpdateUi();
        StateController.Transition(State.INVENTORY);
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
            UIManager.inventorySubmenuUi.Leave();
            StateController.Transition(State.INVENTORY);
        }

        UpdateUi();
    }

    public void RemoveSelectedHotbarItem()
    {
        RemoveItem(hotbarUi.GetSelectedHotbar());
    }

    public Item GetHotbarItem()
    {
        Item item = itemSlots[hotbarUi.GetSelectedHotbar()].item;
        return item;
    }

    public void RemoveItemForRangedAttack()
    {
        RemoveItem(hotbarUi.GetSelectedHotbar());
    }

    private void InitItemSlots()
    {
        itemSlots = new List<ItemSlot>(hotbarSize + slotCount);
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            itemSlots.Add(new ItemSlot());
        }
        UpdateUi();
    }

    public void ConsumeHotbarItem()
    {
        Item item = itemSlots[hotbarUi.GetSelectedHotbar()].item;
        if (item != null)
        {
            IConsumable consumable = item.GetComponent<IConsumable>();
            if (consumable != null)
            {
                consumable = InstantiateItem(item).GetComponent<IConsumable>();
                consumable.Consume(PlayerController.player);
                RemoveItemFromSlot(itemSlots[hotbarUi.GetSelectedHotbar()]);
            }
        }
    }

    private void UpdateUi(ItemSlot itemSlot = null)
    {
        inventoryUi.UpdateUi(itemSlots, itemSlot);
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
