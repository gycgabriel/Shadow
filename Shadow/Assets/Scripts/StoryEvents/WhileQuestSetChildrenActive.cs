using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileQuestSetChildrenActive : MonoBehaviour
{
    public Quest quest;
    public int chapter;
    public int scenario;
    public bool done = false;

    void Update()
    {
        if (PartyController.quest.id == quest.id && PartyController.quest.isActive && StoryManager.scriptInstance.CheckEvoked(chapter, scenario) && !done)
        {
            done = true;
            foreach (Transform child in transform)
            {
                if (Vector2.Distance(PartyController.activePC.transform.position, child.position) < 1f)
                {
                    child.transform.position += new Vector3(-1f, 0f);
                }
                child.gameObject.SetActive(true);
            }

            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();

        float fadeTime = 1f;
        float initTime = Time.realtimeSinceStartup;
        float currentTime = Time.realtimeSinceStartup;
        float initAlpha = 1f;

        // loop over 1 second
        for (float i = 0; i <= fadeTime; i += (Time.realtimeSinceStartup - initTime))
        {
            if (children == null)
                break;
            foreach (SpriteRenderer child in children)
            {
                if (child == null)
                    break;
                child.color = new Color(child.color[0], child.color[1], child.color[2], i / fadeTime * initAlpha);
                currentTime = Time.realtimeSinceStartup;
                yield return null;
            }
        }
    }
}
