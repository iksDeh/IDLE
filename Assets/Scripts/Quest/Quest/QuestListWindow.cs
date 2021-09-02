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
        //QuestManager.instance.onQuestLogChanged += UpdateStatus;
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

            questStatusIcon.sprite = newQuest.questStatusIcon;


        //quest goal sybole einfügen
        questGoalIcon.sprite = newQuest.questStatusIcon;

    }

    public void UpdateStatus(Quest q)
    {
        if (this != null)
            questStatusIcon.sprite = q.questStatusIcon;
    }
}
