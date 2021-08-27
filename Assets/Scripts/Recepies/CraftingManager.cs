using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    #region Singelton

    public static CraftingManager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("!!! More than one instance of CraftingManager found !!!");

        instance = this;
    }

    #endregion



    public Transform craftingSlots;
    private CraftingSlot[] slots;
    private CraftingLayout craftingData = new CraftingLayout();
    private RecepieManager recepieManager;
    private Inventory inventory;
    public Item item;

    void Start()
    {
        recepieManager = RecepieManager.instance;
        inventory = Inventory.instance; 
    }

    public void Craft()
    {

        int counter = 0;
        slots = craftingSlots.GetComponentsInChildren<CraftingSlot>();
        for (int i = 0; i < craftingData.rows.Length; i++)
        {
            craftingData.rows[i].row = new List<Item>();
            for (int j = 0; j < craftingData.rows.Length; j++)
            {
                if (slots[counter].GetItem() != null)
                    craftingData.rows[i].row.Add(slots[counter].GetItem());
                else
                    craftingData.rows[i].row.Add(null);
                counter++;
            }
        }

        foreach (Recepie recepie in recepieManager.recepieList)
        {
            for (int i = 0; i < recepie.craftingData.rows.Length; i++)
            {
                for (int j = 0; j < recepie.craftingData.rows[i].row.Count; j++)
                {
                    if (recepie.craftingData.rows[i].row[j] != null && craftingData.rows[i].row[j] != null)
                        if (recepie.craftingData.rows[i].row[j].name == craftingData.rows[i].row[j].name)
                        {

                        }
                        else
                        {
                            Debug.Log("Material doesnt match");
                            return;
                        }
                    else if (recepie.craftingData.rows[i].row[j] == null && craftingData.rows[i].row[j] != null)
                    {
                        Debug.Log("Wrong Slot");
                        return;
                    }
                    else if (recepie.craftingData.rows[i].row[j] != null && craftingData.rows[i].row[j] == null)
                    {
                        Debug.Log("Wrong Slot");
                        return;
                    }
                }
            }
            if (!recepie.isLearned)
            {
                RecepieManager.instance.onRecepieLearned(recepie);
            }

            for (int i = 0; i < slots.Length; i++)
                slots[i].Remove();

            inventory.Add(recepie.craftedItem);
            return;
        }
        //if(recepie.craftingData == craftingData)
        //{
        //    if(inventory.Remove(recepie.materialAmount))
        //    {
        //        recepie.SetIsLearned(true);
        //        inventory.Add(recepie.craftedItem);
        //    }
        //}
    }
}