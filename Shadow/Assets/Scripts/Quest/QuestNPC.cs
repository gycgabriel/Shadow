using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : Interactable
{
    public string questTitle;
    public Dialogue defaultDialogue;
    public Dialogue whileQuestDialogue;
    public int chapter;
    public int scenario;

    public override void Interact()
    {
        if (PartyController.quest != null && PartyController.quest.title == questTitle && PartyController.quest.isActive)
        {
            DialogueManager.scriptInstance.StartDialogue(whileQuestDialogue);      // quest ongoing lines
        }
        else if (StoryManager.scriptInstance.CheckEvoked(1, 1))
        {
            DialogueManager.scriptInstance.StartDialogue(defaultDialogue);      // default npc lines
        }
        else
        {
            QuestGiver qg = GetComponent<QuestGiver>();
            ScenarioManager.scriptInstance.PlayScenario(chapter, scenario, delegate () { qg.OpenQuestWindow(); });
            StoryManager.scriptInstance.SetEvoked(chapter, scenario);
        }
    }
}
