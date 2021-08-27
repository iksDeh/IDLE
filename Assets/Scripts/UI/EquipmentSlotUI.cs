using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;
    public GameObject gameObjectEquipmentSlot;
    public EquipmentSlot slot;


    Equipment item;
    public void AddItem(Equipment newItem)
    {
        item = newItem;

        
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public int GetEquipmentSlot()
    {
        return (int)slot;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton()
    {      
        EquipmentManager.instance.Unequip((int)slot);
        ClearSlot();
    }

}
