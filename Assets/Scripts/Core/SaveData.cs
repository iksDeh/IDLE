using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int playerLevel;
    public int playerDamage;
    public int playerInt;
    public int playerAgi;
    public int playerStr;
    public int playerSpr;
    public int playerSta;
    public int playerCurrentXP;
    public int playerCurrentSP;
    public int playerMaxHealth;
    public int playerMovementSpeed;
    public int[] playerPosition;

    public string[] inventoryItems;
    public int[] inventoryItemsLvl;

    public string[] equipedItems;
    public int[] equipedItemsLvl;

    public string[] Quests;
    public int[] QuestsStatus;



    public SaveData()
    {

    }
}
