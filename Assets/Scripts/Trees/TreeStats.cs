using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStats : CharackterStats
{
    public Item item;


    public override void Die()
    {
        base.Die();
        Debug.Log(this.name + " droped " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
