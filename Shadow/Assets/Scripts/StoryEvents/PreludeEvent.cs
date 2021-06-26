using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreludeEvent : StartonSceneLoad
{
    public override int chapter { get; set; } = 0;
    public override int scenario { get; set; } = 0;

    public override void StartEvent()
    {
        GetText.LoadChapter(0);
        GetText.LoadScenario(0);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario(() => {
            SceneManager.LoadScene("ChooseClassScreen");
        });
    }
}
