using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Use as parent to multiple QuestNPC
 * QuestNPC GO must be set active false initially
 * Quest array and GO array must be same length
 */
public class MultiQuestNPC : MonoBehaviour
{
    public Quest[] quest;
    public GameObject[] order;

    void Update()
    {
        for (int i = 0; i < order.Length; i++)
        {
            order[i].SetActive(false);
            if (!StoryManager.scriptInstance.CheckCompletedQuests(quest[i]))
            {
                order[i].SetActive(true);
                break;
            }
            
        }

    }
}
