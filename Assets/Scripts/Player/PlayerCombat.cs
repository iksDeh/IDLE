using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Combat
{
    

    private float autoAttack;

    public override void Attack(CharackterStats targetStats)
    {
        if (autoAttack > 0)
        {
            base.Attack(targetStats);
            Debug.Log("Target got hit with: " + myStats.damage.GetValue());
        }
    }

    public void OnAutoAttack(InputAction.CallbackContext context)
    {
        autoAttack = context.ReadValue<float>();

    }

}
