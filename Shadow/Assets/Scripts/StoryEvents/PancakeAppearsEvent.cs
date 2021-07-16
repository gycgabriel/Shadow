using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeAppearsEvent : MonoBehaviour
{
    private void Update()
    {
        if (PartyController.shadowActive && StoryManager.scriptInstance.CheckEvoked(0, 4))          // tut for switch chara
        {
            if (StoryManager.scriptInstance.CheckEvoked(1, 5))
                return;
            GetText.LoadChapter(1);
            GetText.LoadScenario(5);
            Singleton<ScenarioManager>.scriptInstance.PlayScenario();
            StoryManager.scriptInstance.SetEvoked(1, 5);
        }
    }
}
