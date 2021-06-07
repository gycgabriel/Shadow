using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{
    public Stats startStats;
    public System.Func<int, int> expFormula;
    public Dictionary<string, System.Func<int, int>> statIncreaseFormula;

    public int getExpToNextLevel(int currentLevel)
    {
        return expFormula(currentLevel);
    }

    public void raiseStats(Stats stats, int currentLevel)
    {
        foreach (KeyValuePair<string, System.Func<int, int>> kv in statIncreaseFormula)
        {
            stats.addBaseStat(kv.Key, kv.Value(currentLevel));
        }
    }
}
