using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReqQuestToPass : MonoBehaviour
{
    public Quest afterQuest;
    public Quest beforeQuest;
    
    public Vector2 dirc;

    public int chapterToPlay;
    public int scenarioToPlay;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (StoryManager.scriptInstance == null)
            return;

        if (collision.gameObject.tag != "Player" || collision.gameObject.name != "PlayerColliders")
            return;

        bool isAfterQuest = afterQuest == null || StoryManager.scriptInstance.CheckAcceptedQuests(afterQuest);
        bool isBeforeQuest = beforeQuest == null || !StoryManager.scriptInstance.CheckAcceptedQuests(beforeQuest);

        if (isAfterQuest && isBeforeQuest)
        {
            PartyController.scriptInstance.MovePlayer(dirc);
            StartCoroutine(WaitforMovement());
        }
    }

    IEnumerator WaitforMovement()
    {
        yield return new WaitUntil(delegate () { return !PartyController.activePC.playerMoving; });

        ScenarioManager.scriptInstance.PlayScenario(chapterToPlay, scenarioToPlay);
    }
}
