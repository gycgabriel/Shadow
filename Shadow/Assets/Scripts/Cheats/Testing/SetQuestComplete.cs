using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetQuestComplete : MonoBehaviour
{
    public Quest[] questsToSetComplete;

    void Update()
    {
        if (StoryManager.scriptInstance != null && Input.GetKeyDown(KeyCode.F))
        {
            foreach (Quest q in questsToSetComplete)
            {
                StoryManager.scriptInstance.SetAcceptedQuest(q);
                StoryManager.scriptInstance.SetCompletedQuest(q);
            }

            foreach (int x in new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })
            {
                StoryManager.scriptInstance.SetEvoked(0, x);
            }

            foreach (int x in new int[] { 0, 1, 2, 3, 4, 5, 6, 7 , 8, 9, 10})
            {
                StoryManager.scriptInstance.SetEvoked(1, x);
            }
            
            PrintQuests();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("DungeonBossRoom");
            TransferPlayer.Teleport("bossEntrance", new Vector2(0, 1));
        }
            

    }

    void PrintQuests()
    {
        if (StoryManager.scriptInstance == null)
            return;

        string toPrint = "Completed Quests: ";
        foreach (int qIndex in StoryManager.scriptInstance.completedQuests.Keys)
        {
            toPrint += qIndex + ", ";
        }

        Debug.Log(toPrint);

        toPrint = "Accepted Quests: ";
        foreach (int qIndex in StoryManager.scriptInstance.acceptedQuests.Keys)
        {
            toPrint += qIndex + ", ";
        }

        Debug.Log(toPrint);
    }
}
