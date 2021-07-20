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
        startStats = new Stats(150, 50, 50, 20, 5, 15, 20, 0);
        expFormula = x => Mathf.FloorToInt(Mathf.Pow(x, 3f) + 14f);
        statIncreaseFormula = new Dictionary<string, System.Func<int, int>>()
        {
            {"hp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.5f) + 14f) },
            {"mp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 0.5f) + 10f) },
            {"atk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.4f) + 12f) },
            {"def", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.5f) + 14f) },
            {"matk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 0.5f) + 5f) },
            {"mdef", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.25f) + 14f) },
            {"agi", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 5f) },
            {"luk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 5f) },
        };
    }

    public string toString()
    {
        return "Guardian";
    }

}
