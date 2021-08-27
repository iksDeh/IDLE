using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestManager : MonoBehaviour
{
    #region

    public static QuestManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public class QuestGiverQuest
    {
        public QuestGiver questGiver;
        public List<Quest> quests;
        public QuestGiverQuest(QuestGiver questgiver, Quest quest)
        {
            questGiver = questgiver;
            quests = new List<Quest>();
            quests.Add(quest);

        }

        public void AddQuestToQuestGiver(Quest quest)
        {
            quests.Add(quest);
        }
    }

    public delegate void OnQuestLogChanged(Quest quest);
    public OnQuestLogChanged onQuestLogChanged;
    public delegate void OnQuestCompleted(Quest quest);
    public OnQuestCompleted onQuestCompleted;
    public delegate void OnQuestAccepted(Quest quest);
    public OnQuestAccepted onQuestAccepted;
    public delegate void OnQuestTurnedIn(Quest quest);
    public OnQuestTurnedIn onQuestTurnedIn;
    public event System.Action OnQuestAbdoned;

    Dictionary<QuestGiver, QuestGiverQuest> questGiverQuest;
    List<Quest> quest;
    List<QuestGiver> questGiver;

    public QuestLogWindow qlog;
    int questGoalCounter = 1;

    void Start()
    {
        questGiverQuest = new Dictionary<QuestGiver, QuestGiverQuest>();
        quest = new List<Quest>();
        //questGoal = new List<QuestGoal>();
        questGiver = new List<QuestGiver>();
        qlog.Init();
        //questGiverQuests = new Dictionary<QuestGiver, List<Quest>>();
    }

    public Dictionary<QuestGiver, QuestGiverQuest> GetQuestGiverQuests()
    {
        return questGiverQuest;
    }

    public List<QuestGiver> GetQuestGivers()
    {
        return questGiver;
    }

    public void AddQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        foreach (QuestGoal.Kill obj in lQuest.questGoal.killGoal)
        {
            obj.id = questGoalCounter;
            EnemyManager.instance.SetQuestEnemy(obj.enemy, obj.id);
            questGoalCounter++;
        }

        quest.Add(lQuest);
        questGiver.Add(lQuestGiver);

        if (questGiverQuest.ContainsKey(lQuestGiver))
        {
            questGiverQuest[lQuestGiver].AddQuestToQuestGiver(lQuest);
        }
        else
        {
            questGiverQuest.Add(lQuestGiver, new QuestGiverQuest(lQuestGiver, lQuest));
        }

        if (onQuestAccepted != null)
            onQuestAccepted.Invoke(lQuest);
        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void RemoveQuest(Quest lQuest, QuestGiver lquestGiver)
    {
        //aus Dictonary löschen fehölt !!! 
        foreach (QuestGoal.Kill obj in lQuest.questGoal.killGoal)
        {
            EnemyManager.instance.SetQuestEnemy(obj.enemy, obj.id);

        }

        lquestGiver.RemoveQuest(lQuest);
        quest.Remove(lQuest);

        //   questGoal.Remove(lQuest.questGoal);
        questGiver.Remove(lquestGiver);

        if (lQuest.GetQuestStatus() == QuestStatus.turnedIn)
            if (onQuestTurnedIn != null)
                onQuestTurnedIn.Invoke(lQuest);
        if (lQuest.GetQuestStatus() == QuestStatus.abdoned)
            if (OnQuestAbdoned != null)
                OnQuestAbdoned();

        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void UpdateQuest(List<int> questIDs)
    {
        foreach (QuestGiver qg in questGiverQuest.Keys)
            foreach (Quest q in questGiverQuest[qg].quests)
            {
                q.EnemyKilled(questIDs);
                if(q.questGoal.goalCompleted)
                {
                    q.SetQuestStatus(QuestStatus.completed);
                    if (onQuestCompleted != null)
                        onQuestCompleted.Invoke(q);
                    if (onQuestLogChanged != null)
                        onQuestLogChanged.Invoke(q);
                }
            }


        //foreach (Quest q in quest)
        //    foreach (QuestGoal.Kill obj in q.questGoal.killGoal)
        //    {
        //        foreach (int i in enemy.questIDs)
        //            if (obj.id == i)
        //            {
        //                obj.currenAmount++;
        //                if (obj == obj)
        //                {
        //                    if (onQuestCompleted != null)
        //                        onQuestCompleted.Invoke(q);
        //                    if (onQuestLogChanged != null)
        //                        onQuestLogChanged.Invoke(q);
        //                }
        //                Debug.Log("Updated Questlog");
        //                return;
        //            }
        //    }

    }

    public void QuestCompleted(Quest lQuest, QuestGiver lquestGiver)
    {
        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);

        Debug.Log("Quest Completed go back to QuestGiver");
    }
}
