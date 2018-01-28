using UnityEngine;

public class Item : Interactable
{
    new public string name;
    public uint stackSize;

    PlayerController.OnActioButtonDown savedAction;

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

    /*
    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            savedAction = player.onActionButtonDown;
            player.onActionButtonDown = Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            player.onActionButtonDown = savedAction;
        }
    }*/

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
