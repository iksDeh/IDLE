using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharackterStats
{
    public int stamminaHealthMultipli = 10;
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            armor.AddModifier(newItem.armor);
            damage.AddModifier(newItem.damage);
            agility.AddModifier(newItem.agility);
            strength.AddModifier(newItem.strength);
            stammina.AddModifier(newItem.stammina);
            spirit.AddModifier(newItem.spirit);
            inttiligenz.AddModifier(newItem.intiligenz);

            SetHealth(stamminaHealthMultipli * stammina.GetValue());
        }
        if(oldItem != null)
        {
            SetHealth((stamminaHealthMultipli * stammina.GetValue()) * -1);

            armor.RemoveModifier(oldItem.armor);
            damage.RemoveModifier(oldItem.damage);
            agility.RemoveModifier(oldItem.agility);
            strength.RemoveModifier(oldItem.strength);
            stammina.RemoveModifier(oldItem.stammina);
            spirit.RemoveModifier(oldItem.spirit);
            inttiligenz.RemoveModifier(oldItem.intiligenz);

        }
    }
    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
