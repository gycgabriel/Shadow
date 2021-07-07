using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger a Dialogue
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;           // The dialogue to be triggered

    public void TriggerDialogue()
    {
        Singleton<DialogueManager>.scriptInstance.StartDialogue(dialogue);
    }

    public void TriggerDialogue(System.Action nextAction = null)
    {
        Singleton<DialogueManager>.scriptInstance.StartDialogue(dialogue, nextAction);
    }
}
