using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ItemLootTable
{
    [System.Serializable]
    public class AllItemsLootTable
    {
        public Item item;
        public int amount;
    }

    [System.Serializable]
    public class CurrencyLootTable
    {
        public Currency currency;
        public int amount = 1;
    }

    [System.Serializable]
    public class EquipmentLootTable
    {
        public Equipment equip;
        public int amount = 1;
    }

    public int xpReward = 0;
    public List<AllItemsLootTable> itemRewards;
    public List<CurrencyLootTable> currencyRewards;


}
