using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToCompleteQuest : Interactable
{
    public Quest quest;
    public override void Interact()
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(quest))
            return;

        if (PartyController.quest != null && PartyController.quest.id == quest.id && PartyController.quest.isActive)
            QuestWindow.scriptInstance.OpenCompleted(PartyController.quest, PartyController.questChain);
    }
}
