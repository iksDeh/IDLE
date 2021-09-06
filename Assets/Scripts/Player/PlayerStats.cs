using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharackterStats
{


    double xpNeeded = 0;
    double xpCurrent = 0;
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        xpNeeded = stats.level.GetValue() * stats.level.lvlUpXpNeededMultiplier;
        
    }
    
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            //armor.AddModifier(newItem.armor);
            //damage.AddModifier(newItem.damage);
            //agility.AddModifier(newItem.agility);
            //strength.AddModifier(newItem.strength);
            //stammina.AddModifier(newItem.stammina);
            //spirit.AddModifier(newItem.spirit);
            //inttiligenz.AddModifier(newItem.intiligenz);

           // SetHealth(stamminaHealthMultipli * stammina.GetValue());
        }
        if(oldItem != null)
        {
           // SetHealth((stamminaHealthMultipli * stammina.GetValue()) * -1);

            //armor.RemoveModifier(oldItem.armor);
            //damage.RemoveModifier(oldItem.damage);
            //agility.RemoveModifier(oldItem.agility);
            //strength.RemoveModifier(oldItem.strength);
            //stammina.RemoveModifier(oldItem.stammina);
            //spirit.RemoveModifier(oldItem.spirit);
            //inttiligenz.RemoveModifier(oldItem.intiligenz);

        }
    }
    
    public void OnKill(Enemy enemy)
    {
       xpCurrent += Mathf.Sqrt((enemy.myStats.stats.level.GetValue() * stats.level.lvlUpXpMultiplier));
        if (xpCurrent >= xpNeeded)
        {
            stats.level.AddModifier(1);
            if (onLevelUp != null)
                onLevelUp.Invoke();
            xpNeeded += Mathf.Sqrt((stats.level.GetValue() * stats.level.lvlUpXpNeededMultiplier));
        }
        Debug.Log(xpCurrent + " CurrentXP" +xpNeeded+ " NeededXP on Level " + stats.level.GetValue().ToString());
            
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
