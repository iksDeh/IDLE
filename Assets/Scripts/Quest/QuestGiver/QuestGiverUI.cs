using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestGiverUI : MonoBehaviour
{
   // public delegate void OnInteract(QuestGiverUI qgui);
   // OnInteract onInteract;

    public QuestGiverWindow questGiverWindow;
    public SpriteRenderer questStatus;

    public QuestGiver questGiver;

    private List<Quest> quests;
    private bool interacted = false;
    private bool isAvilable = false;
    private bool isCompleted = false;
    private bool isActiv = false;
    private bool notAvilable = false;
    void Start()
    {
        quests = new List<Quest>();
        QuestManager.instance.onQuestLogChanged += UpdateUI;

        questGiver = this.GetComponent<QuestGiver>();
        foreach (Quest q in questGiver.quest)
        {
            if (q.GetQuestStatus() == QuestStatus.completed)
            {
                questStatus.sprite = questGiver.isCompleted;
                isCompleted = true;
                return;
            }
            else if (q.GetQuestStatus() == QuestStatus.available)
            {
                questStatus.sprite = questGiver.isAvilable;
                isAvilable = true;
            }
            else if (q.GetQuestStatus() == QuestStatus.activ && isAvilable == false)
            {
                questStatus.sprite = questGiver.isActiv;
                isActiv = true;
            }

            quests.Add(q);
        }



    }

    public void UpdateUI(Quest quest)
    {
        isAvilable = false;
        isCompleted = false;
        isActiv = false;
        notAvilable = false;

        foreach(Quest q in quests)
            if (q.GetQuestStatus() == QuestStatus.completed)
            {
                questStatus.sprite = questGiver.isCompleted;
                isCompleted = true;
                return;
            }
            else if (q.GetQuestStatus() == QuestStatus.available)
            {
                isAvilable = true;
            }
            else if (q.GetQuestStatus() == QuestStatus.activ )
            {
                isActiv = true;
            }
            
        if(quest.GetQuestStatus() == QuestStatus.completed)
            questStatus.sprite = questGiver.isCompleted;
        else if (quest.GetQuestStatus() == QuestStatus.available && !isCompleted)
            questStatus.sprite = questGiver.isAvilable;
        else if (quest.GetQuestStatus() == QuestStatus.activ && !isCompleted && !isAvilable)
            questStatus.sprite = questGiver.isActiv;
        else if (quest.GetQuestStatus() == QuestStatus.activ && !isCompleted && !isAvilable && !isActiv)
            questStatus.sprite = questGiver.notAvilable;
    }

    public void OpenQuestWindow()
    {
        if (interacted == false)
        {
            questGiverWindow.gameObject.SetActive(true);
            questGiverWindow.Init(questGiver);
            interacted = true;
        }
    }

    public void CloseQuestWindow()
    {
        if (interacted == true)
        {
            questGiverWindow.gameObject.SetActive(false);
            questGiverWindow.Destroy();
            interacted = false;
        }
    }

}
