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
            if (Singleton<StoryManager>.scriptInstance.CheckEvoked(0, 3))
            {
                done = true;
            }
            else if (PartyController.player.GetComponent<Player>().currentLevel > 1)
            {
                StartEvent();
                done = true;
                Singleton<StoryManager>.scriptInstance.SetEvoked(0, 3);
            }
        }
    }

    public void StartEvent()
    {
        StartCoroutine(WaitForPlayerIdle());       // to let attack animations clear
    }

    IEnumerator WaitForPlayerIdle()
    {
        yield return new WaitUntil(() => !PartyController.activePC.playerAttacking && !PartyController.activePC.playerMoving);

        yield return new WaitForSeconds(0.5f);

        GetText.LoadChapter(0);
        GetText.LoadScenario(3);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario();
    }
}
