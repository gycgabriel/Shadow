using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger a Dialogue
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;           // The dialogue to be triggered

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
