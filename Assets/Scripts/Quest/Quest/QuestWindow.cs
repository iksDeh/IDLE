using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestWindow : MonoBehaviour
{
    public QuestRewardsListWindow rewards;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI description;

    public Button buttonAccept;
    public Button buttonComplete;



    private Quest q;
    private QuestGiver qg;

    void Start()
    {
        QuestManager.instance.onQuestLogChanged += UpdateUI;

    }

    public void UpdateUI(Quest quest)
    {


    }

    public void SetQuest(Quest quest, QuestGiver questGiver)
    {
        if(rewards != null)
            rewards.RemoveAllRewards();
        q = quest;
        qg = questGiver;
        rewards.SetReward(quest);
        questName.text = quest.questName;
        description.text = quest.description;

        if (quest.GetQuestStatus() == QuestStatus.activ)
        {
            buttonAccept.gameObject.SetActive(false);
            buttonComplete.gameObject.SetActive(false);
        }

        else if (quest.GetQuestStatus() == QuestStatus.completed && qg.onQuestGiverInteract)
        {
            buttonAccept.gameObject.SetActive(false);
            buttonComplete.gameObject.SetActive(true);
        }
        else if (quest.GetQuestStatus() == QuestStatus.available)
        {
            buttonAccept.gameObject.SetActive(true);
            buttonComplete.gameObject.SetActive(false);
        }
        else
        {
            buttonAccept.gameObject.SetActive(false);
            buttonComplete.gameObject.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        QuestManager.instance.SetQuestActiv(q, qg);
        rewards.RemoveAllRewards();

        this.gameObject.SetActive(false);
    }

    public void CompleteQuest()
    {
        QuestManager.instance.TurnInQuest(q, qg);

        rewards.RemoveAllRewards();
        this.gameObject.SetActive(false);
    }

    public void Close()
    {
        rewards.RemoveAllRewards();
        this.gameObject.SetActive(false);
    }
}
