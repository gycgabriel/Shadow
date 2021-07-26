using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvas : Singleton<FadeCanvas>
{
    public Animator animator;
    public string levelToLoad;
    public static bool fadeDone;

    public void FadeToScene(string levelName)
    {
        fadeDone = false;
        levelToLoad = levelName;
        animator.SetTrigger("FadeOut");
    }

    void OnFadeOutComplete()
    {
        animator.ResetTrigger("FadeOut");
        Debug.Log(levelToLoad);
        if (levelToLoad == "")
            return;
        SceneManager.LoadScene(levelToLoad);
        levelToLoad = "";
        animator.SetTrigger("FadeIn");
    }


    void Fading()
    {
        fadeDone = false;
    }

    void OnFadeInComplete()
    {
        animator.ResetTrigger("FadeIn");
        fadeDone = true;
    }
}
