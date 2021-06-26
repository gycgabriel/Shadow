using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterClass
{
    public enum allClasses { Guardian, Sorcerer, Kannagi };     // list of all possible character classes

    public string className;
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

    public static CharacterClass getCharClass(string value)
    {
        if (System.Enum.TryParse(value, out allClasses myClass))       // is a class
        {
            switch (value)
            {
                case "Guardian":
                    return new Guardian();
                case "Sorcerer":
                    return new Sorcerer();
                default:
                    return new Guardian();
            }

            // return (CharacterClass) System.Activator.CreateInstance(System.Type.GetType(value));       // instantiate new class based on string
        } 
        else
        {
            return null;
        }
    }
}
