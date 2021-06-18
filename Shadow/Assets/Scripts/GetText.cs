using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/**
 * Get text from JSON file
 */
public class GetText : MonoBehaviour
{
    public string fileName;
    public int scenarioId;

    private Chapter chapter;
    private Scenario scenario;
    private Queue<Dialogue> queue = new Queue<Dialogue>();
    private DialogueManager dm;

    void Start()
    {
        string filepath = Path.Combine(Application.dataPath, "Text", fileName + ".json");
        string jsonString = File.ReadAllText(filepath);
        chapter = JsonUtility.FromJson<Chapter>(jsonString);
        Debug.Log("Loaded chapter " + chapter.id);
        scenario = chapter.data[0];
        Debug.Log("Loaded scenario " + scenario.id);

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
            Debug.Log("END");
            return;
        }

        if (!dm.inDialogue)
        {
            dm.StartDialogue(queue.Dequeue(), () => ContinueScenario());
        }
    }

    public void ContinueScenario()
    {
        PlayScenario();
    }

}
