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

    void Update()
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario) || 
            (hasReq && !StoryManager.scriptInstance.CheckEvoked(reqChapter, reqScenario)))
        {
            return;
        }
        else
        {
            StoryManager.scriptInstance.SetEvoked(chapter, scenario);
            StartCoroutine(StartEvent());
        }
    }

    public virtual IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(0.5f);
        ScenarioManager.scriptInstance.PlayScenario(chapter, scenario);
    }

}
