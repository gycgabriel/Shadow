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

    // deprecated: need to drag in Text folder to Build
    public static void LoadChapter(int chapterNum)
    {
        string filepath = Path.Combine(Application.dataPath, "Text", filePrefix + chapterNum + ".json");
        string jsonString = File.ReadAllText(filepath);
        chapter = JsonUtility.FromJson<Chapter>(jsonString);
    }

    // deprecated: need to drag in Text folder to Build
    public static void LoadScenario(int scenarioIndex)
    {
        scenario = chapter.data[scenarioIndex];
        ScenarioManager.scriptInstance.InitScenario(scenario);
    }

    public static void Load(TextAsset jsonText, int scenarioIndex)
    {
        string jsonString = jsonText.text;
        chapter = JsonUtility.FromJson<Chapter>(jsonString);
        scenario = chapter.data[scenarioIndex];
        ScenarioManager.scriptInstance.InitScenario(scenario);
    }

}
