using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXOnEnable : MonoBehaviour
{
    public string sfx;
    public float delay = 0f;

    void OnEnable()
    {
        if (AudioManager.scriptInstance == null)
            return;

        if (delay == 0f)
            AudioManager.scriptInstance.PlaySFX(sfx);
        else
            StartCoroutine(WaitForDelay());
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(delay);

        AudioManager.scriptInstance.PlaySFX(sfx);
    }

}
