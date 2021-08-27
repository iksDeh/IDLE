using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : CharackterStats
{

    Item item;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

    }

    void PickUp()
    {
        Debug.Log(this.transform.name + " droped " + item.name);
        inventory.Add(item);
        Destroy(gameObject);
    }
}
