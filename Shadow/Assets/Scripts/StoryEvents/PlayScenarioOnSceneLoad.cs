using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScenarioOnSceneLoad : MonoBehaviour
{
    public int chapter;
    public int scenario;

    public bool hasReq;
    public int reqChapter;
    public int reqScenario;

    public bool done = false;

    void Update()
    {
        if (!done)
        {
            if (Singleton<StoryManager>.scriptInstance.CheckEvoked(chapter, scenario))
            {
                done = true;
            }
            else if (hasReq && !StoryManager.scriptInstance.CheckEvoked(reqChapter, reqScenario))
            {
                return;         // required story not played yet
            }
            else
            {
                StartEvent();
                done = true;
                Singleton<StoryManager>.scriptInstance.SetEvoked(chapter, scenario);
            }
        }
    }

    public void StartEvent()
    {
        GetText.LoadChapter(chapter);
        GetText.LoadScenario(scenario);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
    }
}
