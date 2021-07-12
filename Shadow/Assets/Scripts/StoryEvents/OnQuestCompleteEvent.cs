using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnQuestCompleteEvent : MonoBehaviour
{
    public string QuestName;
    public int chapter;
    public int scenario;
    
    public bool giveNextQuest;
    public string nextScene;
    public Vector3 newCoords = new Vector3(0, 0, 0);
    public Vector2 directionToFace = new Vector2(0, 1);

    public bool done;

    private void Update()
    {
        if (!done)
        {
            if (Singleton<StoryManager>.scriptInstance.CheckEvoked(chapter, scenario))
            {
                done = true;
            }
            else if (PartyController.quest.title == QuestName && !PartyController.quest.isActive)
            {
                StartEvent();
                done = true;
                Singleton<StoryManager>.scriptInstance.SetEvoked(chapter, scenario);
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

        GetText.LoadChapter(chapter);
        GetText.LoadScenario(scenario);
        Singleton<ScenarioManager>.scriptInstance.PlayScenario(delegate () {
            if (nextScene != "")
            {
                SceneManager.LoadScene(nextScene);
                TransferPlayer.Teleport(newCoords, directionToFace);
            }
            if (giveNextQuest)
            {
                GetComponent<QuestGiver>().OpenQuestWindow();
            }
        });
    }
}
