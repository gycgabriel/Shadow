using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/**
 * Get text from JSON file
 */
public static class GetText
{
    private static string filePrefix = "chapter";               // chapter0.json, chapter1.json...

    private static Chapter chapter;
    private static Scenario scenario;

    public static void LoadChapter(int chapterNum)
    {
        string filepath = Path.Combine(Application.dataPath, "Text", filePrefix + chapterNum + ".json");
        string jsonString = File.ReadAllText(filepath);
        chapter = JsonUtility.FromJson<Chapter>(jsonString);
        Debug.Log("Loaded chapter " + chapter.id);
    }

    public static void LoadScenario(int scenarioIndex)
    {
        scenario = chapter.data[scenarioIndex];
        Debug.Log("Loaded scenario " + scenario.id);
        Singleton<ScenarioManager>.scriptInstance.InitScenario(scenario);
    }

}
