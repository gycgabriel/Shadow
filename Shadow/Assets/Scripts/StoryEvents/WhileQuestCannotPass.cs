using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestCannotPass : MonoBehaviour
{
    public Quest quest;

    public Vector2 dirc;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" || collision.gameObject.name != "PlayerColliders")
            return;

        if (PartyController.quest.title == quest.title && PartyController.quest.isActive)
        {
            PartyController.scriptInstance.MovePlayer(dirc);
            StartCoroutine(WaitforMovement());
        }
    }

    IEnumerator WaitforMovement()
    {
        yield return new WaitUntil(delegate() { return !PartyController.activePC.playerMoving; });

        ScenarioManager.scriptInstance.PlayScenario(0, 6);
    }
}
