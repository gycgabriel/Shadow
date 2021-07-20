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
        startStats = new Stats(100, 100, 5, 10, 50, 15, 20, 0);
        expFormula = x => Mathf.FloorToInt(Mathf.Pow(x, 3f) + 14f);
        statIncreaseFormula = new Dictionary<string, System.Func<int, int>>()
        {
            {"hp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 10f) },
            {"mp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 10f) },
            {"atk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 0.5f) + 5f) },
            {"def", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 5f) },
            {"matk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.5f) + 15f) },
            {"mdef", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1.25f) + 8f) },
            {"agi", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 5f) },
            {"luk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 1f) + 5f) },
        };
    }

    public string toString()
    {
        return "Sorcerer";
    }

}
