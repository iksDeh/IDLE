using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestLogUI : MonoBehaviour
{
    public Transform questLogWindow;
    private QuestLogWindow qlw;

    //public Transform parentQuestWindow;
    //public QuestListWindow questWindow;

    //private QuestManager qm;
    //private Dictionary<Quest, GameObject> quests;
    private bool isInit = false;
    //void Start()
    //{

    //    qm.onQuestLogChanged += UpdateUI;
    // //   qm.OnQuestAccepted += Init;
    //}
    //public void UpdateUI(Quest quest)
    //{

    //    foreach (Quest q in quests.Keys)
    //    {
    //        if (quest == q)
    //            quests[q].GetComponent<QuestListWindow>().UpdateStatus(quest);
    //    }
    //}

    //public void Init()
    //{
    //    if(qm == null)
    //        qm = QuestManager.instance;

    //    if (quests != null)
    //        foreach (Quest q in quests.Keys)
    //            Destroy(quests[q]);
    //    quests = new Dictionary<Quest, GameObject>();
    //    //movement = context.ReadValue<Vector2>();
    //    foreach (QuestGiver qg in qm.GetQuestGiverQuests().Keys)
    //        foreach(Quest q in qm.GetQuestGiverQuests()[qg].quests)
    //    {
    //        GameObject questObj = new GameObject(q.questName);
    //        questObj = Instantiate(questWindow.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
    //        questObj.transform.SetParent(parentQuestWindow);
    //        questObj.transform.localScale = new Vector3(1, 1, 1);
    //        questObj.name = q.questName;
    //        questObj.SetActive(true);
    //        //quests.Add(questObj);

    //        questObj.GetComponent<QuestListWindow>().AddQuest(q, qg);

    //        quests.Add(q, questObj);
    //    }
    //}

    public void OnQuestLogOPen(InputAction.CallbackContext context)
    {
        if(isInit == false)
        {
            //questLogWindow.GetComponent<QuestLogWindow>().Init();
            questLogWindow.gameObject.SetActive(true);
            isInit = true;
        }
        else
        questLogWindow.gameObject.SetActive(!questLogWindow.gameObject.activeSelf);
    }

    public void Close()
    {
        questLogWindow.gameObject.SetActive(false);
    }
}
