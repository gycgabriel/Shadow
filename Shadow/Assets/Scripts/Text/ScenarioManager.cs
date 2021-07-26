using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : Singleton<ScenarioManager>
{
    public TextAsset chapter0;
    public TextAsset chapter1;
    private Queue<Dialogue> queue = new Queue<Dialogue>();
    private System.Action onScenarioEnd = null;

    public void InitScenario(Scenario scenario)
    {
        Dialogue[] dialogues = scenario.data;

        foreach (Dialogue dialogue in dialogues)
        {
            queue.Enqueue(dialogue);
        }
    }
 
    public void PlayScenario(int chapter, int scenario, System.Action nextAction = null)
    {
        if (chapter == -1)
        {
            nextAction?.Invoke();
            return;
        }

        if (chapter == 0)
        {
            GetText.Load(chapter0, scenario);
        }
        else if (chapter == 1)
        {
            GetText.Load(chapter1, scenario);
        }
        PlayScenario(nextAction);
    }

    private void PlayScenario(System.Action nextAction = null)
    {
        // Assign to keep track for future ContinueText() from button press
        this.onScenarioEnd = nextAction;

        if (queue.Count == 0)
        {
            Singleton<DialogueManager>.scriptInstance.scenarioOngoing = false;
            Singleton<DialogueManager>.scriptInstance.EndDialogue();
            Debug.Log("Scenario ended");
            if (onScenarioEnd != null)
            {
                Debug.Log(onScenarioEnd);
                onScenarioEnd();
            }
        }
        else
        {
            Singleton<DialogueManager>.scriptInstance.scenarioOngoing = true;
            Singleton<DialogueManager>.scriptInstance.StartDialogue(queue.Dequeue());
        }
    }

    /**
     * For assigning to buttons when playing scenario
     */
    public void ContinueText()
    {
        // Clicking while in dialogue
        if (Singleton<DialogueManager>.scriptInstance.inDialogue)
        {
            Singleton<DialogueManager>.scriptInstance.ContinueDialogue();
        }
        else
        {
            PlayScenario(this.onScenarioEnd);
        }
    }
}
