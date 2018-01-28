using UnityEngine;

public class Item : Interactable
{
    new public string name;
    public uint stackSize;

    public void Awake()
    {
        if(stackSize <= 0)
        {
            stackSize = 1;
        }
    }

    public void Start()
    {
        if (transform.parent != ItemPool.itemPool.transform)
        {
            transform.SetParent(ItemPool.itemPool.transform);
        }
    }

    public bool Apply()
    {
        return true;
    }

    public override void Interact()
    {
        PlayerController player = PlayerController.player;
        if (player.inventory.AddItem(this))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(player.transform);
        }
    }
}
