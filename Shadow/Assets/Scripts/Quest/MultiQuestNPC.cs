using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Use as parent to multiple QuestNPC
 * All QuestNPC GO must be set active false initially
 * Quest array and GO array must be same length
 */
public class MultiQuestNPC : MonoBehaviour
{
    public Quest[] acceptedQuest;
    public Quest[] notCompletedQuest;
    public GameObject[] order;

    void Update()
    {
        for (int i = 0; i < order.Length; i++)
        {
            if (i == order.Length - 1)          // all else false, then last one as default interactable
            {
                order[i].SetActive(true);
                break;
            }

            bool haveAccepted = (acceptedQuest[i] == null) || StoryManager.scriptInstance.CheckAcceptedQuests(acceptedQuest[i]);
            bool haveNotCompleted = (notCompletedQuest[i] == null) || !StoryManager.scriptInstance.CheckCompletedQuests(notCompletedQuest[i]);

            if (haveAccepted && haveNotCompleted)
            {
                order[i].SetActive(true);
                break;
            }

            order[i].SetActive(false);
        }

    }
}
