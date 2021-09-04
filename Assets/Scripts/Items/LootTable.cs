using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootTable
{
        public Item item;
        public int dropChance = 100;
        public int minDropAmount = 1;
        public int maxDropAmount = 1;
                
    public Item GetLoot()
    {
        int role = Random.Range(dropChance, 100);
        if (role >= dropChance)
            return item;
        else
            return null;
    }
}
