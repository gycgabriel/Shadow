using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : Singleton<StoryManager>
{
    public Dictionary<int, Dictionary<int, bool>> evokedStory = new Dictionary<int, Dictionary<int, bool>>();

    public void SetEvoked(int chapter, int scenario)
    {
        if (!evokedStory.ContainsKey(chapter))
        {
            evokedStory.Add(chapter, new Dictionary<int, bool>());
        }
        evokedStory[chapter].Add(scenario, true);
    }

    public bool CheckEvoked(int chapter, int scenario)
    {
        // Debug.Log("" + chapter + " "+ scenario);
        if (!evokedStory.ContainsKey(chapter))
        {
            evokedStory.Add(chapter, new Dictionary<int, bool>());
            return false;
        }
        else if (!evokedStory[chapter].ContainsKey(scenario))
        {
            return false;
        } else
        {
            return evokedStory[chapter][scenario];
        }
    }


}
