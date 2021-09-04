using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





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

    public Dictionary<Item,int> itemList;

    public int space = 20;
    public List<Currency> currencys;
    private Dictionary<Currency, int> wallet;

    void Start()
    {
        itemList = new Dictionary<Item, int>();
        wallet = new Dictionary<Currency, int>();
        foreach (Currency cur in currencys)
            wallet.Add(cur, 0);
    }

    public bool AddMoney(Currency cur)
    {
        wallet[cur] += cur.amount;
        if(wallet[cur] >= cur.maxCurrency)
        {
            wallet[cur] -= cur.maxCurrency;
            foreach (Currency currency in wallet.Keys)
                if (currency.hirachyID == cur.hirachyID + 1)
                {
                    wallet[currency] += 1;
                    return true;
                }
        }
        return false;
    }

    public bool RemoveMoney(Currency cur)
    {

        if (wallet[cur] - cur.amount >= 0)
        {
            wallet[cur] -= cur.amount;
            return true;
        }
        else
        {
            int currencyID = 0;
            foreach (Currency currency in wallet.Keys)
            {
                if (currency.hirachyID > cur.hirachyID && wallet[currency] > 0)
                    if (currencyID == 0)
                        currencyID = currency.hirachyID;
                    else if (currencyID > currency.hirachyID)
                        currencyID = currency.hirachyID;
            }
            if (currencyID > 0)
            {
                for (int i = currencyID; currencyID >= cur.hirachyID; i--)
                    foreach (Currency currency in wallet.Keys)
                        if (currency.hirachyID == i)
                        {
                            if (currency.hirachyID == cur.hirachyID)
                                wallet[currency] += cur.maxCurrency - cur.amount;
                            else if (currency.hirachyID == currencyID)
                                wallet[currency] -= 1;
                            else
                                wallet[currency] += currency.maxCurrency - 1;
                        }
                return true;
            }
            else
                return false;
        }

    }

    public bool Add(List<ItemLootTable.AllItemsLootTable> qItems)
    {
        if(qItems != null)
        {
            foreach (ItemLootTable.AllItemsLootTable i in qItems)
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
