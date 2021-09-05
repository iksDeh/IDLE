using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class LootTable
{
    [System.Serializable]
    public class CraftingMaterialLootTable
    {
        public Item item;
        public int dropChance = 100;
        public int minDropAmount = 1;
        public int maxDropAmount = 1;
    }

    [System.Serializable]
    public class CurrencyLootTable
    {
        public Currency currency;
        public int dropChance = 100;
        public int minDropAmount = 1;
        public int maxDropAmount = 1;
    }

    [System.Serializable]
    public class EquipmentLootTable
    {
        public Equipment equip;
        public int dropChance = 100;
        public int minDropAmount = 1;
        public int maxDropAmount = 1;
    }

    [HideInInspector] private Dictionary<Item, int> items;
    [SerializeField] private List<CurrencyLootTable> currency;
    [SerializeField] private List<EquipmentLootTable> equip;
    [SerializeField] private List<CraftingMaterialLootTable> craftingMaterial;

    private bool GetLoot(int dropChance)
    {
        if (Random.Range(1, 100) < dropChance)
            return true;
        else
            return false;
    }

    public Dictionary<Item, int> GetItems()
    {
        items = new Dictionary<Item, int>();
        if (currency.Count > 0)
        {
            bool hasRoled = false;
            bool getLoot = false;
            foreach (CurrencyLootTable item in currency)
            {
                if (hasRoled == false)
                {
                    hasRoled = true;
                    getLoot = GetLoot(item.dropChance);
                }
                if(getLoot == true)
                    items.Add(item.currency, Random.Range(item.minDropAmount, item.maxDropAmount));
            }
        }
        if (equip.Count > 0)
            foreach (EquipmentLootTable item in equip)
                if(GetLoot(item.dropChance))
                    items.Add(item.equip, Random.Range(item.minDropAmount, item.maxDropAmount));

        if (craftingMaterial.Count > 0)
            foreach (CraftingMaterialLootTable item in craftingMaterial)
                if (GetLoot(item.dropChance))
                    items.Add(item.item, Random.Range(item.minDropAmount, item.maxDropAmount));

        return items;
    }
}
