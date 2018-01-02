[System.Serializable]
public class ItemSlot
{
    public Item item;
    public int quantity;

    public ItemSlot(Item item)
    {
        this.item = item;
        this.quantity = 1;
    }
}
