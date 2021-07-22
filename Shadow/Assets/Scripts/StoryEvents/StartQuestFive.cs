using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuestFive : MonoBehaviour
{
    public Quest quest;
    public int chapter = 1;
    public int scenario = 11;

    void Update()
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(quest) && StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
        {
            StoryManager.scriptInstance.SetEvoked(chapter, scenario);
            StartCoroutine(StartEvent());
        }
    }

    IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(0.5f);
        ScenarioManager.scriptInstance.PlayScenario(chapter, scenario, delegate() { 
            // teleport to rain's house?
            QuestWindow.scriptInstance.Open(quest); 
        });
    }
}
