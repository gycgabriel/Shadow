using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestChain
{
    public List<Quest> quests;

    public int currentQuest;

    public bool IsEnd()
    {
        return currentQuest >= quests.Count;
    }


    public Quest Next()
    {
        currentQuest++;
        return quests[currentQuest];
    }

    public Quest First()
    {
        currentQuest = 0;
        return quests[currentQuest];
    }

}
