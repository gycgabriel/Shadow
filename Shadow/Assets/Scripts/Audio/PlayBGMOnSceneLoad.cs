using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGMOnSceneLoad : MonoBehaviour
{
    public Sound bgm;
    private bool done;

    void Update()
    {
        if (AudioManager.scriptInstance != null && done != true)
        {
            AudioManager.scriptInstance.PlayBGM(bgm);
            done = true;
        }
    }
}
