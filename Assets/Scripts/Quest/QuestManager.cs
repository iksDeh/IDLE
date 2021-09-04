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
    public delegate void OnQuestAbdoned(Quest quest);
    public OnQuestAbdoned onQuestAbdoned;

    [SerializeField] private Sprite isAvilableIcon;
    [SerializeField] private Sprite isNotAvilableIcon;
    [SerializeField] private Sprite isActivIcon;
    [SerializeField] private Sprite isCompletedIcon;
    [HideInInspector] public Sprite statusIcon;

    public Dictionary<QuestGiver, List<Quest>> activQuestGiverQuests { get; private set; }
    public Dictionary<QuestGiver, List<Quest>> completedQuestGiverQuests { get; private set; }
    public Dictionary<QuestGiver,List<Quest>> notAvilableQuestGiverQuest { get; private set; }
    public Dictionary<QuestGiver, List<Quest>> avilableQuestGiverQuest { get; private set; }

    public QuestLogWindow qlog;
    int questGoalCounter = 1;

    void Start()
    {
        qlog.Init();

        activQuestGiverQuests = new Dictionary<QuestGiver, List<Quest>>();
        completedQuestGiverQuests = new Dictionary<QuestGiver, List<Quest>>();
        notAvilableQuestGiverQuest = new Dictionary<QuestGiver, List<Quest>>();
        avilableQuestGiverQuest = new Dictionary<QuestGiver, List<Quest>>();
    }

    public void AddQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        if (avilableQuestGiverQuest.ContainsKey(lQuestGiver))
            avilableQuestGiverQuest[lQuestGiver].Add(lQuest);
        else
        {
            List<Quest> quests = new List<Quest>();
            quests.Add(lQuest);
            avilableQuestGiverQuest.Add(lQuestGiver, quests);
        }

        lQuest.SetQuestStatus(QuestStatus.available);
        lQuest.questStatusIcon = isAvilableIcon;
    }

    //Add Quest to Dictonary
    //Set QuestStaus & QuestIcon
    //Call AcceptEvent
    //Call UpdateEvent
    public void SetQuestActiv(Quest lQuest, QuestGiver lQuestGiver)
    {
        avilableQuestGiverQuest[lQuestGiver].Remove(lQuest);
        if (avilableQuestGiverQuest[lQuestGiver].Count <= 0)
            avilableQuestGiverQuest.Remove(lQuestGiver);

        if (activQuestGiverQuests.ContainsKey(lQuestGiver))
            activQuestGiverQuests[lQuestGiver].Add(lQuest);
        else
        {
            List<Quest> quests = new List<Quest>();
            quests.Add(lQuest);
            activQuestGiverQuests.Add(lQuestGiver, quests);
        }

        lQuest.SetQuestStatus(QuestStatus.activ);
        lQuest.questStatusIcon = isActivIcon;

        if (onQuestAccepted != null)
            onQuestAccepted.Invoke(lQuest);
        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }
    public void AbdoneQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        activQuestGiverQuests[lQuestGiver].Remove(lQuest);
        if (activQuestGiverQuests[lQuestGiver].Count <= 0)
            activQuestGiverQuests.Remove(lQuestGiver);
        if (avilableQuestGiverQuest.ContainsKey(lQuestGiver))
            avilableQuestGiverQuest[lQuestGiver].Add(lQuest);
        else
        {
            List<Quest> quests = new List<Quest>();
            quests.Add(lQuest);
            avilableQuestGiverQuest.Add(lQuestGiver, quests);
        }

        lQuest.SetQuestStatus(QuestStatus.abdoned);
        lQuest.questStatusIcon = isAvilableIcon;

        if (onQuestAbdoned != null)
            onQuestAbdoned.Invoke(lQuest);

        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void TurnInQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        activQuestGiverQuests[lQuestGiver].Remove(lQuest);
        if (activQuestGiverQuests[lQuestGiver].Count <= 0)
            activQuestGiverQuests.Remove(lQuestGiver);

        if (completedQuestGiverQuests.ContainsKey(lQuestGiver))
            completedQuestGiverQuests[lQuestGiver].Add(lQuest);
        else
        {
            List<Quest> quests = new List<Quest>();
            quests.Add(lQuest);
            completedQuestGiverQuests.Add(lQuestGiver, quests);
        }

        lQuest.SetQuestStatus(QuestStatus.turnedIn);
        lQuest.questStatusIcon = null;

        Inventory.instance.Add(lQuest.questRewards.itemRewards);
       // Inventory.instance.AddMoney(lQuest.questRewards.currencyRewards);

        if (onQuestTurnedIn != null)
            onQuestTurnedIn.Invoke(lQuest);

        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void CompletedQuest(Quest lQuest, QuestGiver lQuestGiver)
    {
        lQuest.SetQuestStatus(QuestStatus.completed);
        lQuest.questStatusIcon = isCompletedIcon;

        if (onQuestCompleted != null)
            onQuestCompleted.Invoke(lQuest);

        if (onQuestLogChanged != null)
            onQuestLogChanged.Invoke(lQuest);
    }

    public void UpdateQuest(Enemy enemy)
    {
        foreach (QuestGiver qg in activQuestGiverQuests.Keys)
            foreach (Quest q in activQuestGiverQuests[qg])
            {
               q.EnemyKilled(enemy);
                if(q.questGoal.goalCompleted)
                {
                    CompletedQuest(q, qg);
                }
            }
    }
}
