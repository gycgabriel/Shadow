using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartonSceneLoad : MonoBehaviour
{
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            Singleton<ScenarioManager>.scriptInstance.PlayScenario();
            done = true;
        }
    }
}
