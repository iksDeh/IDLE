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

    public delegate void OnQuestLogChanged(Quest quest);
    public OnQuestLogChanged onQuestLogChanged;
    public delegate void OnQuestCompleted(Quest quest);
    public OnQuestCompleted onQuestCompleted;
    public delegate void OnQuestAccepted(Quest quest);
    public OnQuestAccepted onQuestAccepted;
    public delegate void OnQuestTurnedIn(Quest quest);
    public OnQuestTurnedIn onQuestTurnedIn;
    public event System.Action OnQuestAbdoned;


    public Sprite isAvilableIcon;
    public Sprite isNotAvilableIcon;
    public Sprite isActivIcon;
    public Sprite isCompletedIcon;

    QuestGiversQuests qgq;
    public Dictionary<QuestGiver, List<Quest>> activQuestGiverQuests { get; private set; }
    public Dictionary<QuestGiver, List<Quest>> completedQuestGiverQuests { get; private set; }
    public QuestLogWindow qlog;
    int questGoalCounter = 1;

    void Start()
    {
        qgq = new QuestGiversQuests();
        qlog.Init();
    }

    public Dictionary<QuestGiver, List<Quest>> GetQuestGiverQuests()
    {
        return qgq.activQuestGiverQuests;
    }

    public List<QuestGiver> GetQuestGivers()
    {
        return qgq.GetQuestGivers();
    }

    public void AddQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        foreach (QuestGoal.Kill obj in lQuest.questGoal.killGoal)
        {
            obj.id = questGoalCounter;
            EnemyManager.instance.SetQuestEnemy(obj.enemy, obj.id);
            questGoalCounter++;
        }

        qgq.AddQuestToQuestGiver(lQuestGiver, lQuest);

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



        //   questGoal.Remove(lQuest.questGoal);
        //questGiver.Remove(lquestGiver);

        if (lQuest.GetQuestStatus() == QuestStatus.turnedIn)
        {

                    lquestGiver.RemoveQuest(lQuest);
       // quest.Remove(lQuest);
            if (onQuestTurnedIn != null)
                onQuestTurnedIn.Invoke(lQuest);
        }

        if (lQuest.GetQuestStatus() == QuestStatus.abdoned)
            if (OnQuestAbdoned != null)
                OnQuestAbdoned();

        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void UpdateQuest(List<int> questIDs)
    {
        foreach (QuestGiver qg in activQuestGiverQuests.Keys)
            foreach (Quest q in activQuestGiverQuests[qg])
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
    }

    public void QuestCompleted(Quest lQuest, QuestGiver lquestGiver)
    {
        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);

        Debug.Log("Quest Completed go back to QuestGiver");
    }



    private class QuestGiversQuests
    {
        public Dictionary<QuestGiver, List<Quest>> activQuestGiverQuests;
        public Dictionary<QuestGiver, List<Quest>> completedQuestGiverQuests;

        public QuestGiversQuests()
        {
            activQuestGiverQuests = new Dictionary<QuestGiver, List<Quest>>();
            completedQuestGiverQuests = new Dictionary<QuestGiver, List<Quest>>();
        }

        public void SetQuestCompleted(QuestGiver qg, Quest q)
        {
            activQuestGiverQuests[qg].Remove(q);
            if (completedQuestGiverQuests.ContainsKey(qg))
                completedQuestGiverQuests[qg].Add(q);
            else
            {
                List<Quest> quests = new List<Quest>();
                quests.Add(q);
                completedQuestGiverQuests.Add(qg, quests);
            }
        }
        public List<QuestGiver> GetQuestGivers()
        {
            List<QuestGiver> qgs = new List<QuestGiver>();
            foreach (QuestGiver qg in activQuestGiverQuests.Keys)
                qgs.Add(qg);
            return qgs;
        }
        public void AddQuestToQuestGiver(QuestGiver qg, Quest q)
        {
            if (activQuestGiverQuests.ContainsKey(qg))
                activQuestGiverQuests[qg].Add(q);
            else
            {
                List<Quest> quests = new List<Quest>();
                quests.Add(q);
                activQuestGiverQuests.Add(qg, quests);
            }
        }
    }
}
