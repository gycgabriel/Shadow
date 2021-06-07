using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Information unique to Guardian class
 */
public class Guardian : CharacterClass
{
    void Awake()
    {
        startStats = new Stats(100, 50, 20, 20, 20, 20, 20, 0);
        expFormula = x => Mathf.FloorToInt(Mathf.Pow(x, 3f) + 14f);
        statIncreaseFormula = new Dictionary<string, System.Func<int, int>>()
        {
            {"hp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"mp", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"atk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"def", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"matk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"mdef", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"agi", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
            {"luk", x =>  Mathf.FloorToInt(Mathf.Pow(x, 2f) + 14f) },
        };
    }

}
