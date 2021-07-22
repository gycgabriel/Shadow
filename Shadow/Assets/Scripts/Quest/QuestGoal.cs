using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public string what;
    public int requiredAmt;
    public int currentAmt;

    public bool IsReached()
    {
        return (currentAmt >= requiredAmt);
    }

    // tag is enemy name
    public void EnemyKilled(string tag)
    {
        if (goalType == GoalType.Kill && (tag == what || what == ""))       // blank means anything
            currentAmt++;
    }

    public void ItemGet(string tag, int amt)
    {
        if (goalType == GoalType.Gathering && (tag == what || what == ""))       // blank means anything
            currentAmt = amt;
    }
}

public enum GoalType
{
    Kill, 
    Gathering, 
    Script
}
