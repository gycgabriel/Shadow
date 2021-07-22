using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestOnCollideScenario : MonoBehaviour
{
    public Quest quest;
    public int chapter;
    public int scenario;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            return;

        if (PartyController.quest.id == quest.id && PartyController.quest.isActive)
        {
            ScenarioManager.scriptInstance.PlayScenario(chapter, scenario);
            StoryManager.scriptInstance.SetEvoked(chapter, scenario);
        }
    }
}
