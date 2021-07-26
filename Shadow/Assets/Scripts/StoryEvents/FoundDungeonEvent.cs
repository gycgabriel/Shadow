using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundDungeonEvent : MonoBehaviour
{
    public Quest quest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(quest))
            return;

        if (PartyController.quest.id == quest.id && PartyController.quest.isActive)
        {
            PartyController.AddExperience(quest.expReward);
            PartyController.AddGold(quest.goldReward);
            QuestWindow.scriptInstance.OpenCompleted(PartyController.quest, PartyController.questChain);
        }
    }
}
