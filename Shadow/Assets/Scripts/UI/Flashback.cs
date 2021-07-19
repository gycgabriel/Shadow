using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashback : Singleton<Flashback>
{
    public Image img;
    public float initAlpha;

    public void On(System.Action action = null)
    {
        initAlpha = img.color[3];
        StartCoroutine(FadeImage(false, action));
    }

    public void Off(System.Action action = null)
    {
        StartCoroutine(FadeImage(true, action));
    }

    IEnumerator FadeImage(bool fadeOut, System.Action action = null)
    {
        float fadeTime = 4f;
        float initTime = Time.realtimeSinceStartup;
        float currentTime = Time.realtimeSinceStartup;
        if (fadeOut)
        {
            // loop over 4 seconds backwards
            for (float i = fadeTime; i >= 0; i -= (currentTime - initTime))
            {
                Debug.Log("fading out");
                img.color = new Color(img.color[0], img.color[1], img.color[2], i/fadeTime * initAlpha);
                currentTime = Time.realtimeSinceStartup;
                yield return null;
            }
            img.gameObject.SetActive(false);
            img.color = new Color(img.color[0], img.color[1], img.color[2], initAlpha);
        }
        else
        {
            img.color = new Color(img.color[0], img.color[1], img.color[2], 0);
            img.gameObject.SetActive(true);
            // loop over 4 seconds
            for (float i = 0; i <= fadeTime; i += (Time.realtimeSinceStartup - initTime))
            {
                Debug.Log("fading in");
                img.color = new Color(img.color[0], img.color[1], img.color[2], i/fadeTime * initAlpha);
                currentTime = Time.realtimeSinceStartup;
                yield return null;
            }
        }
        action?.Invoke();
    }
}
