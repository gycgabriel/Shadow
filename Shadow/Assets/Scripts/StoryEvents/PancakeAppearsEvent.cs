using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeAppearsEvent : MonoBehaviour
{
    private void Update()
    {
        if (StoryManager.scriptInstance.CheckEvoked(1, 5))
            return;
        if (PartyController.shadowActive && StoryManager.scriptInstance.CheckEvoked(0, 4))          // tut for switch chara
        {
            ScenarioManager.scriptInstance.PlayScenario(1, 5);
            StoryManager.scriptInstance.SetEvoked(1, 5);
        }
    }
}
