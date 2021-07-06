using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attach to GO with Dialogue Trigger and QuestGiver
 */
public class QuestInteractable : Interactable
{
    public override void Interact()
    {
        QuestGiver qg = GetComponent<QuestGiver>();
        GetComponent<DialogueTrigger>().TriggerDialogue(delegate() { qg.OpenQuestWindow(); });
    }
}
