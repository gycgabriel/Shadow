using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestActiveEvent : MonoBehaviour
{
    public Quest quest;
    public int chapter;
    public int scenario;

    void Update()
    {
        if (PartyController.quest.title == quest.title)
        {
            if (!StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            {
                ScenarioManager.scriptInstance.PlayScenario(chapter, scenario);
                StoryManager.scriptInstance.SetEvoked(chapter, scenario);
            }
        }
    }
}
