using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFirstLevelUpEvent : MonoBehaviour
{
    public bool done;

    private void Update()
    {
        if (!done)
        {
            if (Singleton<StoryManager>.scriptInstance.CheckEvoked(0, 4))
            {
                done = true;
            }
            else if (PartyController.player.GetComponent<Player>().currentLevel > 1)
            {
                StartEvent();
                done = true;
                Singleton<StoryManager>.scriptInstance.SetEvoked(0, 4);
            }
        }
    }

    public void StartEvent()
    {
        GetText.LoadChapter(0);
        GetText.LoadScenario(4);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
    }
}
