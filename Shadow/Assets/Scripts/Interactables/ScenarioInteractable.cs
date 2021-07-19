using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Play scenario on interact
 */
public class ScenarioInteractable : Interactable
{
    public int chapter;
    public int scenario;

    public int reqChapter;
    public int reqScenario;

    public override void Interact()
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            return;
        if (reqChapter != 0 && reqScenario != 0 && StoryManager.scriptInstance.CheckEvoked(reqChapter, reqScenario))
        {
            ScenarioManager.scriptInstance.PlayScenario(chapter, scenario);
            StoryManager.scriptInstance.SetEvoked(chapter, scenario);
        }
    }
}
