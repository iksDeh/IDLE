using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject equipmentUI;

    EquipmentManager equipManager;
    EquipmentSlotUI[] slots;

    // Start is called before the first frame update
    void Start()
    {
        equipManager = EquipmentManager.instance;
        equipManager.onEquipmentChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlotUI>();
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {

        for (int i = 0; i < slots.Length; i++)
        {
        //    Debug.Log("slots nr :" + (int)slots[i].GetEquipmentSlot() + " | newItem nr : " + (int)newItem.equipSlot);
            if(newItem != null)
                if( (int)slots[i].slot == (int)newItem.equipSlot)
                        slots[i].AddItem(newItem);
            if (oldItem != null)
                if ((int)slots[i].slot == (int)oldItem.equipSlot)
                    slots[i].ClearSlot();
            

        }
    }

    public void OnRemove()
    {
        equipmentUI.SetActive(!equipmentUI.activeSelf);
    }


}
