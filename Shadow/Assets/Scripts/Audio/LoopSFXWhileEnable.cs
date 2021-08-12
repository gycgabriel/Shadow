using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSFXWhileEnable : MonoBehaviour
{
    public string sfx;
    public float delay = 0f;
    private AudioSource thisSFX;

    void OnEnable()
    {
        if (AudioManager.scriptInstance == null)
            return;

        if (delay == 0f)
            thisSFX = AudioManager.scriptInstance.PlaySFX(sfx);
        else
            StartCoroutine(WaitForDelay());
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(delay);

        thisSFX = AudioManager.scriptInstance.PlaySFX(sfx);
    }

    void OnDisable()
    {
        thisSFX.Stop();
    }
}
