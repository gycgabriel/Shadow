using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class StartonSceneLoad : MonoBehaviour
{
    public bool done = false;
    public abstract int chapter { get; set; }
    public abstract int scenario { get; set; }

    void Update()
    {
        if (!done)
        {
            if (Singleton<StoryManager>.scriptInstance.CheckEvoked(chapter, scenario))
            {
                done = true;
            }
            else if (FadeCanvas.fadeDone)
            {
                StartEvent();
                done = true;
                Singleton<StoryManager>.scriptInstance.SetEvoked(chapter, scenario);
            }
        }
    }

    public virtual void StartEvent()
    {

    }
}
