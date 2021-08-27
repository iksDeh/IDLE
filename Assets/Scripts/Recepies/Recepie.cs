using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class CraftingLayout
{

    [System.Serializable]
    public struct rowData
    {
        public List<Item> row;
    }

    public rowData[] rows = new rowData[6]; //Grid of 7x7
}



[CreateAssetMenu(fileName = "New Recepie", menuName = "Inventory/Recepie")]
public class Recepie : ScriptableObject
{

    public string name = "New Recepie";
    public bool isLearned { get; private set; } = false;
    public CraftingLayout craftingData = new CraftingLayout();
    
    public Item craftedItem;

    public Dictionary<Item, int> materialAmount = new Dictionary<Item, int>();
    public bool isDefaultItem = false;



    public void Init()
    {
        isLearned = false;
        bool isKey = false;
        for(int i = 0; i < craftingData.rows.Length; i++)
        {
            for(int j = 0; j < craftingData.rows[i].row.Count; j++)
            {
                if(craftingData.rows[i].row[j] != null)
                if(materialAmount.ContainsKey(craftingData.rows[i].row[j]))
                    materialAmount[craftingData.rows[i].row[j]]++;
                else
                    materialAmount.Add(craftingData.rows[i].row[j], 1);
                //foreach(Item item in materialAmount.Keys)
                //    if (item.name == craftingData.rows[i].row[j].name)
                //    {
                //        isKey = true;
                //        materialAmount[craftingData.rows[i].row[j]]++;
                //    }
                //if (isKey == false)                   
                //        materialAmount.Add(craftingData.rows[i].row[j], 1);
                //else
                //    isKey = false;
            }
        }
    }

    public void SetIsLearned(bool value)
    {
        isLearned = value;
    }

    public virtual void Craft()
    {
        try
        {
            
           // requiredItems.ForEach(requiredItems => Inventory.instance.Remove(requiredItems));
          //  Inventory.instance.Add(craftedItem);
        }
        catch
        {
            Debug.Log("Not enough Items");
        }
        //Use required Items and add crafted Item

    }
}
