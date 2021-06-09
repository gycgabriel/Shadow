using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Base stats system for all creatures in the game
 */
[System.Serializable]
public class Stats
{
    private Dictionary<string, int> baseStats;
    private Dictionary<string, int> modifiedStats;          // recalculated each time stats is accessed

    public Stats(int hp, int mp, int atk, int def, int matk, int mdef, int agi, int luk)
    {
        baseStats = new Dictionary<string, int>()
        {
            {"hp", hp },
            {"mp", mp },
            {"atk", atk },
            {"def", def },
            {"matk", matk },
            {"mdef", mdef },
            {"agi", agi },
            {"luk", luk },
        };
    }

    public Dictionary<string, int> getStats(Dictionary<string, StatModifier> modifierList)
    {
        modifiedStats = new Dictionary<string, int>(baseStats);             // create a copy
        StatModifier.sum(modifierList).apply(modifiedStats);                // in-place stat changes
        return modifiedStats;
    }

    public void addBaseStat(string stat, int value)             // add stat on level up
    {
        baseStats[stat] += value;
    }

}
