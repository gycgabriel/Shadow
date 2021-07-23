using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToCompleteQuestRemoveItem : TalkToCompleteQuest
{
    public Item itemToRemove;

    public override void Interact()
    {
        base.Interact();
        PartyController.inventory.Remove(itemToRemove, true);
    }
}
