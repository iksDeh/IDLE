using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvesting : Interactable
{
    [System.Serializable]
    public class HarvestingLootTable
    {
        public Item item;
        public int minDropAmount = 1;
        public int maxDropAmount = 1;
        public HarvestingLootTable(Item item, int min, int max)
        {
            this.item = item;
            minDropAmount = min;
            maxDropAmount = max;
        }


    }
    public List<HarvestingLootTable> lootTable;



}
