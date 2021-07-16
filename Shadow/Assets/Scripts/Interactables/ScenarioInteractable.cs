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

    public override void Interact()
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            return;
        GetText.LoadChapter(chapter);
        GetText.LoadScenario(scenario);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
        StoryManager.scriptInstance.SetEvoked(chapter, scenario);
    }
}
