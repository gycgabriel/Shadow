using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Attach to GO with Dialogue Trigger
 */
public class DialogueInteractable : Interactable
{
    public override void Interact()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
