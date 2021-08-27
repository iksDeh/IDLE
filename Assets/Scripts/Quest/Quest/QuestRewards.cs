using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestRewards
{
    [System.Serializable]
    public class QuestItemReward
    {
        public Item item;
        public int amount;
    }

    public int xpReward = 0;
    public Currency currencyRewards;
    public List<QuestItemReward> itemRewards;

}


