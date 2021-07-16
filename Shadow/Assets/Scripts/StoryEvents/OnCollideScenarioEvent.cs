using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideScenarioEvent : MonoBehaviour
{
    public int chapter;
    public int scenario;
    public int reqChapter = 0;
    public int reqScenario = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            return;
        if (!StoryManager.scriptInstance.CheckEvoked(reqChapter, reqScenario))
            return;
        GetText.LoadChapter(chapter);
        GetText.LoadScenario(scenario);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
        StoryManager.scriptInstance.SetEvoked(chapter, scenario);
    }
}
