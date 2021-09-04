using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ItemLootTable
{
    [System.Serializable]
    public class CraftingMaterialLootTable
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

    [HideInInspector] public  Dictionary<Item, int> items;
    [SerializeField] private List<CurrencyLootTable> currency;
    [SerializeField] private List<EquipmentLootTable> equip;
    [SerializeField] private List<CraftingMaterialLootTable> craftingMaterial;


    public void Init()
    {
        items = new Dictionary<Item, int>();
        if (currency.Count > 0)
            foreach (CurrencyLootTable item in currency)
                items.Add(item.currency, item.amount);
        if (equip.Count > 0)
            foreach (EquipmentLootTable item in equip)
                items.Add(item.equip, item.amount);
        if (craftingMaterial.Count > 0)
            foreach (CraftingMaterialLootTable item in craftingMaterial)
                items.Add(item.item, item.amount);
    }

}
