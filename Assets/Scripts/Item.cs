using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;
    public uint stackSize;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        if(stackSize <= 0)
        {
            stackSize = 1;
        }
    }

    public bool Apply()
    {
        Debug.Log("Applied item " + name);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            player.onActionButtonDown += PickUp;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (coll.GetComponent<PlayerController>())
        {
            player.onActionButtonDown -= PickUp;
        }
    }

    private void PickUp()
    {
        PlayerController player = PlayerController.player;
        if (player.inventory.AddItem(this))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(player.transform);
        }
    }
}
