using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;



[System.Serializable]
public class QuestGoal
{
    [System.Serializable]
    public class Kill
    {
        public Enemy enemy;
        public int reqiredAmount;
        public int currenAmount = 0;
        [HideInInspector] public int id = 0;
    }

    [System.Serializable]
    public class Gather
    {
        public Item item;
        public int reqiredAmount;
        [HideInInspector] public int currenAmount = 0;
        [HideInInspector] public int id = 0;
    }

    [System.Serializable]
    public class Loot
    {
        public Enemy enemy;
        public Item item;
        public int requiredAmount;
        [HideInInspector] public int currenAmount = 0;
        [HideInInspector] public int id = 0;
    }

    public List<Kill> killGoal = new List<Kill>();
    public List<Loot> lootGoal; 
    public List<Gather> gatherGoal;

    [HideInInspector] public bool killGoalIsReached  = false;
    [HideInInspector] public bool lootGoalIsReached  = false;
    [HideInInspector] public bool gatherGoalIsReached  = false;

    [HideInInspector] public bool goalCompleted = false;

    public void EnemyKilled(List<int> questIDs)
    {
        if (killGoal.Count == 0)
            killGoalIsReached = true;
        if (lootGoal.Count == 0)
            lootGoalIsReached = true;
        if (gatherGoal.Count == 0)
            gatherGoalIsReached = true;

        foreach(Kill goal in killGoal)
            Debug.Log(goal.currenAmount);
        foreach (int i in questIDs)
        {
            if(killGoal.Count != 0)
            foreach (Kill goal in killGoal)
            {
                    Debug.Log(goal.currenAmount);
                if (goal.id == i)
                   goal.currenAmount++;
                if (goal.currenAmount >= goal.reqiredAmount)
                    killGoalIsReached = true;
            }
            if (lootGoal.Count != 0)
                foreach (Loot goal in lootGoal)
            {
                // if (goal.enemy.id == enemy.id)
                // Drop QuestItem (random) 
            }
        }

        if (killGoalIsReached && lootGoalIsReached && gatherGoalIsReached)
            goalCompleted = true;
    }
}

public enum GoalType
{
    Kill,
    Gathering,
    Looting

}