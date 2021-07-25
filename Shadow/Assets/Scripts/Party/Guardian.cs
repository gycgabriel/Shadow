using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Information unique to Guardian class
 */
[System.Serializable]
public class Guardian : CharacterClass
{
    public Guardian()
    {
        className = "Guardian";
        startStats = new Stats(150, 50, 40, 60, 5, 30, 20, 0);
        expFormula = x => Mathf.RoundToInt(Mathf.Pow(x, 2.75f) + 49 * x);
        statIncreaseFormula = new Dictionary<string, System.Func<int, int>>()
        {
            {"hp", x =>  LogGrowthFormula(x, 75) - LogGrowthFormula(x - 1, 75) },
            {"mp", x =>  LogGrowthFormula(x, 50) - LogGrowthFormula(x - 1, 50) },
            {"atk", x =>  LogGrowthFormula(x, 80) - LogGrowthFormula(x - 1, 80)  },
            {"def", x =>  LogGrowthFormula(x, 90) - LogGrowthFormula(x - 1, 90) },
            {"matk", x =>  LogGrowthFormula(x, 0) },
            {"mdef", x =>  LogGrowthFormula(x, 45) - LogGrowthFormula(x - 1, 45) },
            {"agi", x =>  LogGrowthFormula(x, 0) },
            {"luk", x =>  LogGrowthFormula(x, 0) },
        };
    }

    public string toString()
    {
        return "Guardian";
    }

}
