using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Information unique to Sorcerer class
 */

[System.Serializable]
public class Sorcerer : CharacterClass
{
    public Sorcerer()
    {
        className = "Sorcerer";
        startStats = new Stats(100, 100, 5, 30, 50, 50, 20, 0);
        expFormula = x => Mathf.RoundToInt(Mathf.Pow(x, 2.75f) + 49 * x);
        statIncreaseFormula = new Dictionary<string, System.Func<int, int>>()
        {
            {"hp", x =>  LogGrowthFormula(x, 50) - LogGrowthFormula(x - 1, 50) },
            {"mp", x =>  LogGrowthFormula(x, 100) - LogGrowthFormula(x - 1, 100) },
            {"atk", x =>  LogGrowthFormula(x, 0) },
            {"def", x =>  LogGrowthFormula(x, 45) - LogGrowthFormula(x - 1, 45) },
            {"matk", x =>  LogGrowthFormula(x, 100) - LogGrowthFormula(x - 1, 100) },
            {"mdef", x =>  LogGrowthFormula(x, 75) - LogGrowthFormula(x - 1, 75) },
            {"agi", x =>  LogGrowthFormula(x, 0) },
            {"luk", x =>  LogGrowthFormula(x, 0) },
        };
    }

    public string toString()
    {
        return "Sorcerer";
    }

}
