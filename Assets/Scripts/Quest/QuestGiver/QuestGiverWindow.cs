using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverWindow : MonoBehaviour
{
    public GameObject questTransform;
    
    public Transform questParent;

    //private List<GameObject> quests;
    private Dictionary<Quest, GameObject> quests;
    void Start()
    {
        QuestManager.instance.onQuestLogChanged += UpdateUI;
        QuestManager.instance.onQuestTurnedIn += QuestTurnedIn;

    }

    public void Init(QuestGiver questGiver)
    {
        if (quests != null)
            foreach (Quest q in quests.Keys)
                Destroy(quests[q]);

        quests = new Dictionary<Quest, GameObject>();

        for (int i = 0; i < questGiver.quest.Count; i++)
        {
            if (questGiver.quest[i].GetQuestStatus() != QuestStatus.turnedIn)
                if (questParent.GetComponentInChildren<Transform>().name != questGiver.quest[i].questName)
                {
                    GameObject questObj;
                    questObj = Instantiate(questTransform.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                    questObj.transform.SetParent(questParent);
                    questObj.transform.localScale = new Vector3(1, 1, 1);
                    questObj.name = questGiver.quest[i].questName;
                    questObj.SetActive(true);

                    questObj.GetComponent<QuestListWindow>().AddQuest(questGiver.quest[i], questGiver);

                    quests.Add(questGiver.quest[i], questObj);

                }
        }
    }

    public void UpdateUI(Quest quest)
    {
        if (this.gameObject.activeInHierarchy)
            quests[quest].GetComponent<QuestListWindow>().UpdateStatus(quest);
    }

    public void Destroy()
    {
        if (quests != null && quests.Count > 0)
            foreach (Quest q in quests.Keys)
                Destroy(quests[q].gameObject);
    }

    public void QuestTurnedIn(Quest quest)
    {
        quests[quest].GetComponent<QuestListWindow>().RemoveQuest();
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
