using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Single-time quest
 */
public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public QuestChain questChain;

    public GameObject questAvailablePrefab;

    private GameObject instantiatedPrefab;

    void Update()
    {
        if (questChain != null)
            quest = questChain.First();

        if (instantiatedPrefab == null && !StoryManager.scriptInstance.CheckAcceptedQuests(quest))
            instantiatedPrefab = Instantiate(questAvailablePrefab, transform, false);
        else if (instantiatedPrefab != null && StoryManager.scriptInstance.CheckAcceptedQuests(quest))
            Destroy(instantiatedPrefab);
    }


    public void OpenQuestWindow()
    {
        if (QuestWindow.scriptInstance == null)
            return;

        if (questChain == null)
        {
            QuestWindow.scriptInstance.Open(quest);
        } 
        else
        {
            QuestWindow.scriptInstance.Open(questChain.First(), questChain);
        }
    }

}
