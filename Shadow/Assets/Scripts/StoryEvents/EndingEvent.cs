using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingEvent : MonoBehaviour
{
    public Quest questSix;
    public AudioSource bgmToStop;
    public AudioSource bgmToPlay;

    void Update()
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(questSix) && !StoryManager.scriptInstance.CheckEvoked(1, 15))
        {
            StartCoroutine(StartEvent());
            StoryManager.scriptInstance.SetEvoked(1, 15);
        }
            
    }

    IEnumerator StartEvent()
    {

        yield return new WaitForSeconds(0.5f);

        bgmToStop.Stop();
        bgmToPlay.loop = true;
        bgmToPlay.Play();

        // set night time / darken screen?
        ScenarioManager.scriptInstance.PlayScenario(1, 15, delegate ()
        {
            ScenarioManager.scriptInstance.PlayScenario(1, 16, delegate ()
            {
                StoryManager.scriptInstance.SetEvoked(1, 16);
                // ending screen and credits roll
                FadeCanvas.scriptInstance.FadeToScene("Credits");
            });
        });
    }
}
