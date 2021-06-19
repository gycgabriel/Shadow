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

    void Start()
    {
        string filepath = Path.Combine(Application.dataPath, "Text", fileName + ".json");
        string jsonString = File.ReadAllText(filepath);
        chapter = JsonUtility.FromJson<Chapter>(jsonString);
        Debug.Log("Loaded chapter " + chapter.id);
        scenario = chapter.data[0];
        Debug.Log("Loaded scenario " + scenario.id);
        Singleton<ScenarioManager>.scriptInstance.InitScenario(scenario);
    }

}
