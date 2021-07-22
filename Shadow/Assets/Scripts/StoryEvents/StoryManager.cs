using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : Singleton<StoryManager>
{
    public Dictionary<int, Dictionary<int, bool>> evokedStory = new Dictionary<int, Dictionary<int, bool>>();
    public Dictionary<int, bool> acceptedQuests = new Dictionary<int, bool>();
    public Dictionary<int, bool> completedQuests = new Dictionary<int, bool>();

    public void SetEvoked(int chapter, int scenario)
    {
        if (!evokedStory.ContainsKey(chapter))
        {
            evokedStory.Add(chapter, new Dictionary<int, bool>());
        }
        else if (evokedStory[chapter].ContainsKey(scenario))
        {
            evokedStory[chapter][scenario] = true;
        }
        else
        {
            evokedStory[chapter].Add(scenario, true);
        }
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

    public bool CheckAcceptedQuests(Quest quest)
    {
        if (acceptedQuests.ContainsKey(quest.id))
            return acceptedQuests[quest.id];
        else
            return false;
    }

    public void SetAcceptedQuest(Quest quest)
    {
        if (acceptedQuests.ContainsKey(quest.id))
            acceptedQuests[quest.id] = true;
        else
            acceptedQuests.Add(quest.id, true);
    }

    public bool CheckCompletedQuests(Quest quest)
    {
        if (completedQuests.ContainsKey(quest.id))
            return completedQuests[quest.id];
        else
            return false;
    }

    public void SetCompletedQuest(Quest quest)
    {
        if (completedQuests.ContainsKey(quest.id))
            completedQuests[quest.id] = true;
        else
            completedQuests.Add(quest.id, true);
    }


}
