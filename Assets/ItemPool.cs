using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool itemPool = null;

    public void Awake()
    {
        if (itemPool == null)
        {
            itemPool = this;
        }
        if(itemPool != this)
        {
            Destroy(this.gameObject);
        }
    }
}
