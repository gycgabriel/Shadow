using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : Interactable
{
    public Quest quest;
    public Dialogue whileQuestDialogue;
    public Dialogue defaultDialogue;

    public override void Interact()
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(quest))
        {
            DialogueManager.scriptInstance.StartDialogue(defaultDialogue);
            return;
        }

        if (PartyController.quest != null && PartyController.quest.id == quest.id && PartyController.quest.isActive)
        {
            DialogueManager.scriptInstance.StartDialogue(whileQuestDialogue);      // quest ongoing lines
        }
        else if (!StoryManager.scriptInstance.CheckAcceptedQuests(quest))
        {
            QuestGiver qg = GetComponent<QuestGiver>();
            qg.OpenQuestWindow();
        }
    }
}
