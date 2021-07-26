using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cecilia NPC
 */
public class CeciliaInteractable : Interactable
{
    public override void Interact()
    {
        if (PartyController.quest != null && PartyController.quest.title == "Kill 10 slimes" && PartyController.quest.isActive)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();      // TO replace with quest ongoing lines
        }
        else if (StoryManager.scriptInstance.CheckEvoked(1, 1))
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();      // default cecilia lines
        }
        else
        {
            QuestGiver qg = GetComponent<QuestGiver>();
            ScenarioManager.scriptInstance.PlayScenario(1, 1, delegate () { qg.OpenQuestWindow(); });
            StoryManager.scriptInstance.SetEvoked(1, 1);
        }
    }

}
