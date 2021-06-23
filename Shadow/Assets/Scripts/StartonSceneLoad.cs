using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartonSceneLoad : MonoBehaviour
{
    public bool done = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!done)
        {
            GetText.LoadChapter(0);
            GetText.LoadScenario(0);
            Singleton<ScenarioManager>.scriptInstance.PlayScenario(() => {
                Singleton<DialogueManager>.scriptInstance.Reset();
                SceneManager.LoadScene("ChooseClassScreen"); 
            });
            done = true;
        }
    }
}
