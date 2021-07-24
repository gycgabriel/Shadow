using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestSetChildrenActive : MonoBehaviour
{
    public Quest quest;
    public int chapter;
    public int scenario;

    void Update()
    {
        if (PartyController.quest.title == quest.title && PartyController.quest.isActive && StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
        {
            foreach (Transform child in transform)
            {
                if (Vector2.Distance(PartyController.activePC.transform.position, child.position) < 1f)
                {
                    child.transform.position += new Vector3(-1f, 0f);
                }
                child.gameObject.SetActive(true);
            }
        } 
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
