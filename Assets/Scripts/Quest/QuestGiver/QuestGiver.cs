using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : Interactable
{
    //public Quest[] quest;
    public List<Quest> quest;

    private PlayerController player;

    public bool onQuestGiverInteract { get; private set; } = false;
    private void Start()
    {
        player = PlayerController.instance;
        //QuestManager.instance.OnQuestCompleted += CompletedQuest;


        for (int i = 0; i < quest.Count; i++)
        {
            QuestManager.instance.AddQuest(quest[i], this);
            quest[i].Init(this);
        }

    }

    public void RemoveQuest(Quest quest)
    {
        for (int i = 0; i < this.quest.Count; i++)
            if (this.quest[i] == quest)
                this.quest.RemoveAt(i);
    }
    public override void StayInteract()
    {
        base.StayInteract();
        if (GetPlayerInteract())
        {
            this.GetComponent<QuestGiverUI>().OpenQuestWindow();
            onQuestGiverInteract = true;
        }
    }

    public override void ExitInteract()
    {
        base.ExitInteract();
        this.GetComponent<QuestGiverUI>().CloseQuestWindow();
        onQuestGiverInteract = false;
    }
}
