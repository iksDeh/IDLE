using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable
{
    public ParticleSystem part;
    public SortOfWood sortOfWood;
    public int dropAmountMin = 1;
    public int dropAmountMay = 3;

    CharackterStats myStats;
    PlayerManager playermanager;

    void Start()
    {
        playermanager = PlayerManager.instance;
        myStats = GetComponent<CharackterStats>();

    }
    public override void StayInteract()
    {
        base.StayInteract();

        PlayerCombat playerCombat = playermanager.player.GetComponent<PlayerCombat>();
        if (playerCombat != null)
        {

                if(playerCombat.Attack(myStats))
                    part.Play();
            

        }

    }
}

public enum SortOfWood
{
    Oak,
    Birch,
    Anderes
}