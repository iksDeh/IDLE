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
    private bool isNotAvilable = false;
    void Start()
    {
        quests = new List<Quest>();
        QuestManager.instance.onQuestLogChanged += UpdateUI;
        QuestManager.instance.onQuestTurnedIn += TurnedIn;

        questGiver = this.GetComponent<QuestGiver>();
        foreach (Quest q in questGiver.quest)
        {
            ///  if(q.questConditions.lvlRequired <= PlayerController.instance.stats.level.GetValue())
            if (q.GetQuestStatus() == QuestStatus.completed)
            {
                questStatus.sprite = q.questStatusIcon;
                isCompleted = true;
                return;
            }
            else if (q.GetQuestStatus() == QuestStatus.available)
            {
                questStatus.sprite = q.questStatusIcon;
                isAvilable = true;
            }

            else if (q.GetQuestStatus() == QuestStatus.activ && isAvilable == false)
            {
                questStatus.sprite = q.questStatusIcon;
                isActiv = true;
            }

            else if (q.GetQuestStatus() == QuestStatus.activ && isAvilable == false && isActiv == false)
            {
                questStatus.sprite = q.questStatusIcon;
                isNotAvilable = true;
            }

            else if (q.GetQuestStatus() == QuestStatus.turnedIn && isAvilable == false && isActiv == false && isNotAvilable == false)
            {
                questStatus.sprite = q.questStatusIcon;
            }


            quests.Add(q);
        }



    }

    public void TurnedIn(Quest q)
    {
        quests.Remove(q);
    }

    public void UpdateUI(Quest quest)
    {
        if(quests.Count > 0)
        {
            isAvilable = false;
            isCompleted = false;
            isActiv = false;
            isNotAvilable = false;

            foreach (Quest q in quests)
                if (q.GetQuestStatus() == QuestStatus.completed)
                {
                    questStatus.sprite = q.questStatusIcon;
                    isCompleted = true;
                    return;
                }
                else if (q.GetQuestStatus() == QuestStatus.available)
                {
                    questStatus.sprite = q.questStatusIcon;
                    isAvilable = true;
                }

                else if (q.GetQuestStatus() == QuestStatus.activ && isAvilable == false)
                {
                    questStatus.sprite = q.questStatusIcon;
                    isActiv = true;
                }

                else if (q.GetQuestStatus() == QuestStatus.activ && isAvilable == false && isActiv == false)
                {
                    questStatus.sprite = q.questStatusIcon;
                    isNotAvilable = true;
                }

                else if (q.GetQuestStatus() == QuestStatus.turnedIn && isAvilable == false && isActiv == false && isNotAvilable == false)
                {
                    questStatus.sprite = q.questStatusIcon;
                    //quests.Remove(q);
                }
        }
        else
        {
            questStatus.sprite = quest.questStatusIcon;
        }
        


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
