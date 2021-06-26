using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : Singleton<ScenarioManager>
{
    private Queue<Dialogue> queue = new Queue<Dialogue>();
    public static DialogueManager dm;
    private System.Action onScenarioEnd = null;

    public void InitScenario(Scenario scenario)
    {
        Dialogue[] dialogues = scenario.data;

        dm = Singleton<DialogueManager>.scriptInstance;

        foreach (Dialogue dialogue in dialogues)
        {
            queue.Enqueue(dialogue);
        }
    }

    public void PlayScenario(System.Action nextAction = null)
    {
        // Assign to keep track for future ContinueText() from button press
        this.onScenarioEnd = nextAction;

        if (queue.Count == 0)
        {
            dm.scenarioOngoing = false;
            dm.EndDialogue();
            Debug.Log("Scenario ended");
            if (onScenarioEnd != null)
            {
                Debug.Log(onScenarioEnd);
                onScenarioEnd();
            }
        }
        else
        {
            dm.scenarioOngoing = true;
            dm.StartDialogue(queue.Dequeue());
        }
    }

    /**
     * For assigning to buttons when playing scenario
     */
    public void ContinueText()
    {
        // Clicking while in dialogue
        if (dm.inDialogue)
        {
            dm.ContinueDialogue();
        }
        else
        {
            PlayScenario(this.onScenarioEnd);
        }
    }
}
