using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class QuestListWindow : MonoBehaviour
{
    public TextMeshProUGUI questName;
    public Image questGoalIcon;
    public Image questStatusIcon;
    public Transform questWindow;
    //private QuestWindowUI questWindowUI;
    Quest quest;
    QuestGiver questGiver;
    private ClickableObject clickableObject;
    void Start()
    {
        clickableObject = this.GetComponent<ClickableObject>();
        clickableObject.onLeftClick += OpenQuestWindow;

    }

    public void OpenQuestWindow(PointerEventData eventData)
    {
        questWindow.gameObject.SetActive(true);
        questWindow.GetComponent<QuestWindow>().SetQuest(quest, questGiver);

    }

    public void RemoveQuest()
    {
        Destroy(this.gameObject);
    }
    public void AddQuest(Quest newQuest, QuestGiver questGiver)
    {
        this.questGiver = questGiver;
        quest = newQuest;
        questName.text = quest.questName;
        
        
        if (newQuest.GetQuestStatus() == QuestStatus.available)
        {
            questStatusIcon.sprite = questGiver.isAvilable;
        }
        else if(newQuest.GetQuestStatus() == QuestStatus.notAvailable)
        {
            questStatusIcon.sprite = questGiver.notAvilable;
        }
        else if (newQuest.GetQuestStatus() == QuestStatus.activ)
        {
            questStatusIcon.sprite = questGiver.isActiv;
        }
        else if (newQuest.GetQuestStatus() == QuestStatus.completed)
        {
            questStatusIcon.sprite = questGiver.isCompleted;
        }

        //quest goal sybole einfügen
        questGoalIcon.sprite = questGiver.isCompleted;

    }

    public void UpdateStatus(Quest q)
    {
        if (q.GetQuestStatus() == QuestStatus.available)
        {
            questStatusIcon.sprite = questGiver.isAvilable;
        }
        else if (q.GetQuestStatus() == QuestStatus.notAvailable)
        {
            questStatusIcon.sprite = questGiver.notAvilable;
        }
        else if (q.GetQuestStatus() == QuestStatus.activ)
        {
            questStatusIcon.sprite = questGiver.isActiv;
        }
        else if (q.GetQuestStatus() == QuestStatus.completed)
        {
            questStatusIcon.sprite = questGiver.isCompleted;
        }
    }
}
