using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(QuestGoal))]
//[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
//[System.Serializable]
public enum QuestStatus
{
    available,
    notAvailable,
    activ,
    abdoned,
    completed,
    turnedIn
}

//[System.Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{

    public string questName;
    public string description;

    public QuestGoal questGoal;
    public QuestRewards questRewards;
    public QuestCondition questConditions;
    [HideInInspector] public Sprite questStatusIcon;
    private QuestStatus questStatus;
    [HideInInspector] public QuestGiver questGiver;

    public void EnemyKilled(Enemy enemy)
    {
        questGoal.EnemyKilled(enemy);
    }



    public void SetQuestStatus(QuestStatus status)
    {
        questStatus = status;
    }

    public QuestStatus GetQuestStatus()
    {
        return questStatus;
    }
    public void Init(QuestGiver qgiver)
    {
        questGiver = qgiver;
      //  questStatus = QuestStatus.available;

        string overrideName = "";

        if(questGoal.killGoal != null)
        foreach (QuestGoal.Kill qg in questGoal.killGoal)
        {
            overrideName += "\n" + qg.enemy.name + " Amount: " + qg.reqiredAmount;
        }
        overrideName += "\n";

        description = description.Replace("{enemy}", overrideName);

        questGoal.killGoalIsReached = false;
        questGoal.gatherGoalIsReached = false;
        questGoal.lootGoalIsReached = false;
        questGoal.goalCompleted = false;

        foreach (QuestGoal.Kill kill in questGoal.killGoal)
        {
            kill.currenAmount = 0;
            kill.id = 0;
        }
        foreach (QuestGoal.Gather gather in questGoal.gatherGoal)
        {
            gather.currenAmount = 0;
            gather.id = 0;
        }
        foreach (QuestGoal.Loot loot in questGoal.lootGoal)
        {
            loot.currenAmount = 0;
            loot.id = 0;
        }

    }
}
