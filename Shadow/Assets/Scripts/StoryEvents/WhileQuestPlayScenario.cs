using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestPlayScenario : MonoBehaviour
{
    public Quest quest;
    public Dialogue dialogue;
    public bool done;

    void Update()
    {
        if (PartyController.quest.id == quest.id && PartyController.quest.isActive && !done)
        {
            DialogueManager.scriptInstance.StartDialogue(dialogue);
            done = true;
        }
    }
}
