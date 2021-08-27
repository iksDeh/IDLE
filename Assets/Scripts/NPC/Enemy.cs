using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharackterStats))]
public class Enemy : Interactable
{

    PlayerManager playermanager;
    CharackterStats myStats;
    //public int maxEnemy = 5;
    [HideInInspector] public bool isActiveQuestMob { get; set; }
     public List<int> questIDs;
    [HideInInspector]public int id { get; set; }

    void Start()
    {
        playermanager = PlayerManager.instance;
        myStats = GetComponent<CharackterStats>();
        questIDs = new List<int>();
    }
    public override void StayInteract()
    {
        base.StayInteract();

        PlayerCombat playerCombat = playermanager.player.GetComponent<PlayerCombat>();
        if(playerCombat != null)
                playerCombat.Attack(myStats);
            
    }

    public override void StartInteract()
    {
        base.StartInteract();

    }

}
