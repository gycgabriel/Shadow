using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFlowerInteractable : ScenarioInteractable
{
    public GameObject go;
    public GameObject go2;

    public int tutChapter;
    public int tutScenario;

    public Item flower;

    public override void Interact()
    {
        if (StoryManager.scriptInstance.CheckEvoked(chapter, scenario))
            return;
        if (reqChapter != 0 && reqScenario != 0 && StoryManager.scriptInstance.CheckEvoked(reqChapter, reqScenario))
        {
            go2.SetActive(false);
            Flashback.scriptInstance.On(delegate() 
            {
                go.SetActive(true);
                ScenarioManager.scriptInstance.PlayScenario(chapter, scenario, delegate ()
                {
                    StoryManager.scriptInstance.SetEvoked(chapter, scenario);
                    go.SetActive(false);
                    Flashback.scriptInstance.Off(delegate ()
                    {
                        ScenarioManager.scriptInstance.PlayScenario(tutChapter, tutScenario, delegate () 
                        {
                            StoryManager.scriptInstance.SetEvoked(tutChapter, tutScenario);
                            PartyController.inventory.Add(flower);
                        });
                    });
                });
            });
        }
    }

}
