using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestCondition
{
    public int lvlRequired = 0;
    public List<Quest> questReqiured = null;
    public List<Item> itemRequired = null;

    //public int reputationRequired;
    //public Buff buffs;
    //if named mob ist in area;
}
