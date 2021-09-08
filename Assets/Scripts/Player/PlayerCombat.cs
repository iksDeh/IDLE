using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Combat
{
    

    private float autoAttack;

    public override bool Attack(CharackterStats targetStats)
    {
        if (autoAttack > 0)
        {
            Debug.Log("Target got hit with: " + myStats.stats.damage.GetValue());
            return base.Attack(targetStats);
        }
        return false;
    }

    public void OnAutoAttack(InputAction.CallbackContext context)
    {
        autoAttack = context.ReadValue<float>();

    }

}
