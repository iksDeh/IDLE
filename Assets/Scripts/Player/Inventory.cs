using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Currency
{
    public int copper = 0;
    public int silver = 0;
    public int gold = 0;
    public int platin = 0;

    public Sprite imageCopper;
    public Sprite imageSilver;
    public Sprite imageGold;
    public Sprite imagePlatin;

    public Currency(int copper = 0, int silver = 0 , int gold =0 ,int platin = 0)
    {
        this.copper = copper;
        this.silver = silver;
        this.gold = gold;
        this.platin = platin;
    }

    public void AddCurrency(Currency currency)
    {
        copper += currency.copper;
        if (copper >= 100)
        {
            silver++;
            copper -= 100;
        }

        silver += currency.silver;
        if (silver >= 100)
        {
            gold++;
            silver -= 100;
        }

        gold += currency.gold;
        if (gold >= 100)
        {
            platin++;
            gold -= 100;
        }

        platin += currency.platin;
    }

    public bool RemoveCurrency(Currency currency)
    {
        if ((this.copper - currency.copper) >= 0)
        {
            this.copper -= currency.copper;
            return true;
        }
        else if ((this.copper - currency.copper) < 0 && (this.silver > 0 || this.gold > 0 || this.platin > 0))
        {
            if(this.silver > 0)
            {
                this.silver -= 1;
                this.copper += 100 - currency.copper;
            }    
            else if(this.gold > 0 )
            {
                this.gold -= 1;
                this.silver += 99;
                this.copper += 100 - currency.copper;
            }
            else if (this.platin > 0)
            {
                this.platin -= 1;
                this.gold += 99;
                this.silver += 99;
                this.copper += 100 - currency.copper;
            }
            return true;
        }
        if ((this.silver - currency.silver) >= 0)
        {
            this.silver -= currency.silver;
            return true;
        }
        else if ((this.silver - silver) < 0 && (this.gold > 0 || this.platin > 0))
        {
            if (this.gold > 0)
            {
                this.gold -= 1;
                this.silver += 100 - currency.silver;
            }
            else if (this.platin > 0)
            {
                this.platin -= 1;
                this.gold += 99;
                this.silver += 100 - currency.silver;
            }
            return true;
        }
        if ((this.gold - gold) >= 0)
        {
            this.gold -= gold;
            return true;
        }
        else if ((this.gold - currency.gold) < 0 && this.platin > 0)
        {
            this.platin -= 1;
            this.gold += 100 - currency.gold;
            return true;
        }
        return false;
    }
}

public class Inventory : MonoBehaviour
{

    #region Singelton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("!!! More than one instance of Inventory found !!!");

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged(Item item);
    public OnItemChanged onItemChangedCallback;

    public Dictionary<Item,int> itemList = new Dictionary<Item, int>();

    public int space = 20;
    public Currency currency = new Currency();

    public void AddMoney(Currency cur)
    {
        currency.AddCurrency(cur);
    }

    public void RemoveMoney(Currency cur)
    {
        currency.RemoveCurrency(cur);
    }

    public bool Add(List<QuestRewards.QuestItemReward> qItems)
    {
        if(qItems != null)
        {
            foreach (QuestRewards.QuestItemReward i in qItems)
            {
                if (itemList.ContainsKey(i.item))
                    itemList[i.item] += i.amount;
                else
                    itemList.Add(i.item, i.amount);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke(i.item);
            }
            return true;
        }
        return false;
    }

    public bool Add(Item item, int amount)
    {
        if (!item.isDefaultItem)
        {
            if (itemList.Count >= space)
            {
                Debug.Log("Inventory is full");
                return false;
            }
            if (itemList.ContainsKey(item))
                itemList[item] += amount;
            else
                itemList.Add(item, amount);



            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(item);
        }
        return true;
    }

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(itemList.Count >= space)
            {
                Debug.Log("Inventory is full");
                return false;
            }
            if (itemList.ContainsKey(item))
                itemList[item]++;
            else
                itemList.Add(item, 1);



            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(item);
        }
        return true;
    }
    public int GetItemAmount(Item item)
    {
        int i = 0;
         itemList.TryGetValue(item, out i);
        return i;

    }
    public Item GetItem(Item item)
    {
       foreach(Item i in itemList.Keys)
        {
            if (i == item)
                return i;
        }
        return null;
    }
    public bool Remove(Dictionary<Item,int> items)
    {
        try
        {
            foreach (Item i in items.Keys)
                if (itemList[i] - items[i] > 0)
                {
                    itemList[i] -= items[i];
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke(i);
                }

                else if (itemList[i] - items[i] == 0)
                {
                itemList.Remove(i);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke(i);
            }

                else
                {
                    Debug.Log("Not enough Items to craft");
                    return false;
                }

            return true;
        }
        catch
        {
            return false;
        }


    }
    public void Remove(Item item, int amount)
    {
        for(int i = 0;  i < amount; i++)
        {
            if (itemList[item] > 1)
                itemList[item]--;
            else
                itemList.Remove(item);
        }
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke(item);
    }

    public void Remove(Item item)
    {
        if (itemList[item] > 1)
            itemList[item]--;
        else
            itemList.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke(item);
    }
}
