using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestLogWindow : MonoBehaviour
{
    public Transform parentQuestListWindow;
    public QuestListWindow questListWindow;

    private QuestManager qm;
    private Dictionary<Quest, GameObject> quests;

    public void Init()
    {
        if(qm == null)
        {
            qm = QuestManager.instance;
            qm.onQuestLogChanged += UpdateUI;
            qm.onQuestAccepted += AddQuest;
            qm.onQuestTurnedIn += RemoveQuest;
        }
    }

    public void UpdateUI(Quest quest)
    {
        quests[quest].GetComponent<QuestListWindow>().UpdateStatus(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        Destroy(quests[quest]);
    }

    public void AddQuest(Quest quest)
    {
        if (quests != null)
            foreach (Quest q in quests.Keys)
                Destroy(quests[q]);
        quests = new Dictionary<Quest, GameObject>();

        foreach (QuestGiver qg in qm.activQuestGiverQuests.Keys)
            foreach (Quest q in qm.activQuestGiverQuests[qg])
            {
                GameObject questObj = new GameObject(q.questName);
                questObj = Instantiate(questListWindow.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                questObj.transform.SetParent(parentQuestListWindow);
                questObj.transform.localScale = new Vector3(1, 1, 1);
                questObj.name = q.questName;
                questObj.SetActive(true);

                questObj.GetComponent<QuestListWindow>().AddQuest(q, qg);

                quests.Add(q, questObj);

            }
    }
    public void OnQuestLogOpen(InputAction.CallbackContext context)
    {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
