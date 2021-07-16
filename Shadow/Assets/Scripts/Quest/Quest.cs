using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]

public class Quest : ScriptableObject
{
    public bool isActive;

    public string title;
    [TextArea(3, 5)]
    public string desc;
    public int expReward;
    public int goldReward;
    public int startChapter;
    public int startScenario;
    public int endChapter;
    public int endScenario;

    public QuestGoal goal;

    public void Accept()
    {
        isActive = true;
        ScenarioManager.scriptInstance.PlayScenario(startChapter, startScenario);
    }

    public void Complete(System.Action nextQuestAction = null)
    {
        isActive = false;
        Debug.Log("IM QUEST COMPLETE");
        ScenarioManager.scriptInstance.PlayScenario(endChapter, endScenario, nextQuestAction);
    }
}
