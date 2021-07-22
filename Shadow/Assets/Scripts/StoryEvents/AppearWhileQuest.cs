using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhileQuest : MonoBehaviour
{
    public Quest quest;

    void Update()
    {
        if (PartyController.scriptInstance == null)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (PartyController.quest != null && PartyController.quest.title == quest.title && PartyController.quest.isActive)
            sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 1);

        else
            sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], 0);
    }

}
