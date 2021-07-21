using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestChain
{
    public List<Quest> quests;

    public int currentQuest;

    public static QuestChain LoadQuestChain(SerializableQuestChain squest)
    {
        if (squest == null)
            return null;
        QuestChain qc = new QuestChain();
        qc.quests = new List<Quest>();
        foreach (SerializableQuest s in squest.quests)
        {
            qc.quests.Add(Quest.LoadQuest(s));
        }
        qc.currentQuest = squest.currentQuest;
        return qc;
    }

    public SerializableQuestChain SaveQuestChain()
    {
        List<SerializableQuest> xs = new List<SerializableQuest>();
        foreach (Quest q in quests)
        {
            xs.Add(q.SaveQuest());
        }
        return new SerializableQuestChain(xs, currentQuest);
    }

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
