using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharackterStats
{
    public int lvlUpXpMultiplier = 10;
    public int lvlUpXpNeededMultiplier = 100;

    public int stamminaHealthMultipli = 10;


    double xpNeeded = 0;
    double xpCurrent = 0;
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        xpNeeded = level.GetValue() * lvlUpXpNeededMultiplier;
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

    public void OnKill(EnemyStats enemy)
    {
        
        xpCurrent += Mathf.Sqrt((enemy.level.GetValue() * lvlUpXpMultiplier));
        if (xpCurrent >= xpNeeded)
        {
            level.AddModifier(1);
            xpNeeded += Mathf.Sqrt((level.GetValue() * lvlUpXpNeededMultiplier));
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
