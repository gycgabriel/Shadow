using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakheartEvent : StartonSceneLoad
{
    public override int chapter { get; set; } = 0;
    public override int scenario { get; set; } = 3;

    public override void StartEvent()
    {
        GetText.LoadChapter(0);
        GetText.LoadScenario(3);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
    }
}
