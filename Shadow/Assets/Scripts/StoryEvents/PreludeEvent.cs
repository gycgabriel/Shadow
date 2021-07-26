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
        ScenarioManager.scriptInstance.PlayScenario(0, 0, () => {
            SceneManager.LoadScene("ChooseClassScreen");
        });
    }
}
