using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScenarioOnQuestComplete : MonoBehaviour
{
    public Quest quest;
    public int chapter;
    public int scenario;

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
        ScenarioManager.scriptInstance.PlayScenario(chapter, scenario);
    }
}
