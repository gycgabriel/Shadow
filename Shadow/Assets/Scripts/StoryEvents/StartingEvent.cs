using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingEvent : PlayScenarioOnSceneLoad
{
    public override IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(0.5f);
        ScenarioManager.scriptInstance.PlayScenario(chapter, scenario, () => PauseMenu.scriptInstance.ShowHowToPlay());
    }
}
