using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : Singleton<ScenarioManager>
{
    private Queue<Dialogue> queue = new Queue<Dialogue>();
    public static DialogueManager dm;

    public void InitScenario(Scenario scenario)
    {
        Dialogue[] dialogues = scenario.data;

        dm = Singleton<DialogueManager>.scriptInstance;

        foreach (Dialogue dialogue in dialogues)
        {
            queue.Enqueue(dialogue);
        }
    }

    public void PlayScenario()
    {
        if (queue.Count == 0)
        {
            dm.EndDialogue();
            Debug.Log("Scenario ended");
        } 
        else if (queue.Count == 1) 
        {
            dm.StartDialogue(queue.Dequeue(), false);
        }
        else
        {
            dm.StartDialogue(queue.Dequeue(), true);
        }
    }

    public void ContinueText()
    {
        if (dm.inDialogue)
        {
            dm.ContinueDialogue();
        }
        else
        {
            PlayScenario();
        }
    }
}
