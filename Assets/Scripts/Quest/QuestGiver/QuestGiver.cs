using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : Interactable
{
    //public Quest[] quest;
    public List<Quest> quest;

    private PlayerController player;

    public Sprite isAvilable;
    public Sprite isActiv;
    public Sprite isCompleted;
    public Sprite notAvilable;

    public bool onQuestGiverInteract { get; private set; } = false;
    private void Start()
    {
        player = PlayerController.instance;
        //QuestManager.instance.OnQuestCompleted += CompletedQuest;

        for (int i = 0; i < quest.Count; i++)
            quest[i].Init();
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
