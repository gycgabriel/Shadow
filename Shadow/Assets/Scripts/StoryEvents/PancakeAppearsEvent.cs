using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeAppearsEvent : MonoBehaviour
{
    public Quest quest3;

    private void Update()
    {
        if (StoryManager.scriptInstance.CheckEvoked(1, 5))
            return;
        if (StoryManager.scriptInstance.CheckAcceptedQuests(quest3) && PartyController.shadowActive && StoryManager.scriptInstance.CheckEvoked(0, 4))          // tut for switch chara
        {
            ScenarioManager.scriptInstance.PlayScenario(1, 5);
            StoryManager.scriptInstance.SetEvoked(1, 5);
        }
    }
}
