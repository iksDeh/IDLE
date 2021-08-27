using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingUI : MonoBehaviour
{
    public Transform craftingUI;
    public Transform craftingSlots;

    private CraftingSlot[] craftingSlot;

    void Start()
    {
        craftingSlot = craftingSlots.GetComponentsInChildren<CraftingSlot>();
    }
    public void RemoveCraftingSlots()
    {
        for(int i = 0; i < craftingSlot.Length; i++)
        {
            if(craftingSlot[i].GetItem() != null)
            {
                Inventory.instance.Add(craftingSlot[i].GetItem());
                craftingSlot[i].Remove();
            }
        }
    }

    public void OnCraftingUI(InputAction.CallbackContext context)
    {
        bool isActiv = this.gameObject.activeInHierarchy;
        foreach (Transform obj in craftingUI.GetComponentInChildren<Transform>())
        {
            if (!isActiv)
                if (obj.name == "Charackterinfo")
                    obj.gameObject.SetActive(false);
                //else if (obj.name == "Button" && obj.gameObject.activeInHierarchy)
                ////  obj.gameObject.SetActive(true);
                //else if (obj.name == "Inventory" && obj.gameObject.activeInHierarchy) 
                ////obj.gameObject.SetActive(true);
                //else if (obj.name == "Background" && obj.gameObject.activeInHierarchy) 
                ////obj.gameObject.SetActive(true);
                else
                 obj.gameObject.SetActive(true);
                        else
                            obj.gameObject.SetActive(false);
        }

        if(!this.gameObject.activeInHierarchy)
            RemoveCraftingSlots();
    }
}
