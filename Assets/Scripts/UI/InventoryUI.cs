using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryUI;
    public Transform inventorySlots;

    Inventory inventory;
    InventorySlot[] slotList;
    Dictionary<Item, InventorySlot> slots;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = new Dictionary<Item, InventorySlot>();

        slotList = inventorySlots.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot invslot in slotList)
            invslot.transform.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


    }

    void UpdateUI(Item item)
    {

                if (slots.ContainsKey(item))
            for (int i = 0; i < slotList.Length; i++)
            {
                if (slotList[i].GetItem() == item)
                    if(inventory.GetItemAmount(item) > 0)
                    {
                        slotList[i].AddItem(item);
                        slotList[i].transform.gameObject.SetActive(true);
                        return;
                    }
                    else
                    {
                        slots.Remove(item); 
                        slotList[i].ClearSlot();
                        slotList[i].transform.gameObject.SetActive(false);
                        return;
                    }
            }
        else
            for (int i = 0; i < slotList.Length; i++)
            {
                if (slotList[i].GetItem() == null)
                {
                    slots.Add(item, slotList[i]);
                    slotList[i].AddItem(item);
                    slotList[i].transform.gameObject.SetActive(true);
                    return;
                }
            }



        //for (int i = 0; i < slotList.Length; i++)
        //    {
        //        if (i < inventory.itemList.Count)
        //        {
        //            if (slots.ContainsKey(inventory.GetItem(item)))

        //                slotList[i].AddItem(inventory.GetItem(item));
        //            slotList[i].transform.gameObject.SetActive(true);
        //        }
        //        else
        //        {
        //            slotList[i].ClearSlot();
        //            slotList[i].transform.gameObject.SetActive(false);
        //        }

        //    }
    }

    public void OnInventoryUI(InputAction.CallbackContext context)
    {
        OnRemove();
    }
    public void OnRemove()
    {
        slotList[0].Reset();
        bool isActiv = this.gameObject.activeInHierarchy;
        foreach (Transform obj in inventoryUI.GetComponentInChildren<Transform>())
        {
            if (!isActiv)
                if (obj.name == "Crafting")
                {
                    obj.GetComponent<CraftingUI>().RemoveCraftingSlots();
                    obj.gameObject.SetActive(false);
                }
                else if (obj.name == "Button" && !obj.gameObject.activeInHierarchy)
                    obj.gameObject.SetActive(true);
                else if (obj.name == "Inventory" && !obj.gameObject.activeInHierarchy)
                    obj.gameObject.SetActive(true);
                else if (obj.name == "Background" && !obj.gameObject.activeInHierarchy)
                    obj.gameObject.SetActive(true);
                else
                    obj.gameObject.SetActive(true);
            else
                obj.gameObject.SetActive(false);
        }
    }
}
