using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReqScenarioToPass : MonoBehaviour
{
    public int bchapter;
    public int bscenario;

    public int chapter;
    public int scenario;
    public Vector2 dirc;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" || collision.gameObject.name != "PlayerColliders")
            return;

        if (StoryManager.scriptInstance.CheckEvoked(bchapter, bscenario) && !StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
        {
            PartyController.scriptInstance.MovePlayer(dirc);
            StartCoroutine(WaitforMovement());
        }
    }

    IEnumerator WaitforMovement()
    {
        yield return new WaitUntil(delegate () { return !PartyController.activePC.playerMoving; });

        ScenarioManager.scriptInstance.PlayScenario(0, 6);
    }
}
