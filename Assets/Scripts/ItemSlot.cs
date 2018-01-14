[System.Serializable]
public class ItemSlot
{
    public Item item = null;
    public int quantity;

    public ItemSlot()
    {
        item = null;
        quantity = 0;
    }

    public ItemSlot(Item item)
    {
        this.item = item;
        this.quantity = 1;
    }
}
