using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SerializableQuestChain
{
    public List<SerializableQuest> quests;

    public int currentQuest;

    public SerializableQuestChain(List<SerializableQuest> xs, int quest)
    {
        quests = xs;
        currentQuest = quest;
    }
}
