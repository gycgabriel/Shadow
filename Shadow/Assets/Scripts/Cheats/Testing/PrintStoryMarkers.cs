using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PrintStoryMarkers : MonoBehaviour
{
    void Update()
    {
        if (StoryManager.scriptInstance != null && Input.GetKeyDown(KeyCode.RightShift))
        {
            Print();
        }
    }
    void Print()
    {
        if (StoryManager.scriptInstance == null)
            return;

        string toPrint;

        toPrint = "Story Evoked: ";
        foreach (int i in StoryManager.scriptInstance.evokedStory.Keys)
        {
            toPrint += i + ", ";
        }

        Debug.Log(toPrint);

        if (PartyController.quest == null)
            toPrint = "Current Quest: null";

        else
            toPrint = "Current Quest: " + PartyController.quest.id + " " + PartyController.quest.isActive;

        Debug.Log(toPrint);

        toPrint = "Accepted Quests: ";
        foreach (int qIndex in StoryManager.scriptInstance.acceptedQuests.Keys)
        {
            toPrint += qIndex + ", ";
        }

        Debug.Log(toPrint);

        toPrint = "Completed Quests: ";
        foreach (int qIndex in StoryManager.scriptInstance.completedQuests.Keys)
        {
            toPrint += qIndex + ", ";
        }

        Debug.Log(toPrint);

        
    }
}
