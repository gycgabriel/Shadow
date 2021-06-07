using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Player that UI scripts access to change UI.
 * To include in Save file.
 */
public class Player : MonoBehaviour
{
    public CharacterClass charclass;
    public string[] skills;
    public Stats stats;
    public Dictionary<string, StatModifier> statModifiers;          // equipment_name or debuff/buff name as string

    public int currentHP;
    public int currentMP;
    public int currentLevel;
    public int currentExp;
    public int expToNextLevel;                // full exp of this level not accounting for exp already gained;
    

    void Start()
    {
        currentLevel = 1;
        currentExp = 0;
        expToNextLevel = charclass.getExpToNextLevel(currentLevel);
    }

    void Update()
    {
        if (currentExp >= expToNextLevel)       // check if current experience points is enough to level up
        {
            levelUp();
        }
    }

    public void addExperience(int expToAdd)
    {
        currentExp += expToAdd;
    }

    public void levelUp()
    {
        currentExp -= expToNextLevel;           // deduct the experience points needed to level up from current experience points
        currentLevel++;
        expToNextLevel = charclass.getExpToNextLevel(currentLevel);  // set requirement for next level

        charclass.raiseStats(stats, currentLevel);              // stat increase on levelup

        currentHP = stats.getStats(statModifiers)["hp"];        // change current hp to new full hp
        currentMP = stats.getStats(statModifiers)["mp"];
    }
}
