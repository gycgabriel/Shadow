using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearBeforeQuest : MonoBehaviour
{
    public Quest afterQuest;
    public Quest beforeQuest;

    void Update()
    {
        if (StoryManager.scriptInstance == null)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool isAfterQuest = afterQuest == null || StoryManager.scriptInstance.CheckCompletedQuests(afterQuest);
        bool isBeforeQuest = beforeQuest == null || !StoryManager.scriptInstance.CheckAcceptedQuests(beforeQuest);

        if (isAfterQuest && isBeforeQuest)
            sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 1);

        else
            sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 0);
    }
}
