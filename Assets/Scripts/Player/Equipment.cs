using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armor;
    public int damage;
    public int stammina;
    public int agility;
    public int strength;
    public int intiligenz;
    public int spirit;

    


    public override void Use()
    {
        base.Use();
        if(EquipmentManager.instance.Equip(this))
            RemoveFromInventory();

    }
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Shoulder,
    Wist,
    Gloves,
    Legs,
    Boots,
    Ring,
    Amulett,
    MainHandWeapon,
    OffHandWeapon,
    TwoHandWeapon
}
