using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]

public class Quest : ScriptableObject
{
    public int id;

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

    public static Quest LoadQuest(SerializableQuest squest)
    {
        if (squest == null)
            return null;
        Quest q = CreateInstance<Quest>();
        squest.CopyTo(q);
        return q;
    }

    public SerializableQuest SaveQuest()
    {
        SerializableQuest s = new SerializableQuest();
        this.CopyTo(s);
        return s;
    }


    public void Accept()
    {
        isActive = true;
        Debug.Log("QUEST ACCEPTED: " + startChapter + " " + startScenario);
        if (startChapter > 0 && startScenario > 0)
            ScenarioManager.scriptInstance.PlayScenario(startChapter, startScenario, delegate() {
                StoryManager.scriptInstance.SetEvoked(startChapter, startScenario);
            });
    }

    public void Complete(System.Action nextQuestAction = null)
    {
        isActive = false;
        Debug.Log("QUEST COMPLETE: " + endChapter + " " + endScenario);
        if (endChapter > 0 && endScenario > 0)
        {
            ScenarioManager.scriptInstance.PlayScenario(endChapter, endScenario, delegate()
            {
                StoryManager.scriptInstance.SetEvoked(endChapter, endScenario);
                nextQuestAction?.Invoke();
            });
        }
        else
        {
            nextQuestAction();
        }
            
    }
}
