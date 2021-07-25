using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCreditsCredits : MonoBehaviour
{
    void Update()
    {
        if (!StoryManager.scriptInstance.CheckEvoked(1, 17))
        {
            ScenarioManager.scriptInstance.PlayScenario(1, 17);
            StoryManager.scriptInstance.SetEvoked(1, 17);
        }
    }
}
