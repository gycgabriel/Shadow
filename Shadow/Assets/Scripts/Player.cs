using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Player that UI scripts access to change UI.
 * To include in Save file.
 */
public class Player : Creature
{
    public CharacterClass charclass;

    public int currentExp;
    public int expToNextLevel;                // full exp of this level not accounting for exp already gained;


    private void Start()
    {
        chooseCharClass("Sorcerer");
    }

    public void addExperience(int expToAdd)
    {
        currentExp += expToAdd;
        if (currentExp >= expToNextLevel)       // check if current experience points is enough to level up
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        currentExp -= expToNextLevel;           // deduct the experience points needed to level up from current experience points
        currentLevel++;
        expToNextLevel = charclass.getExpToNextLevel(currentLevel);  // set requirement for next level

        charclass.raiseStats(stats, currentLevel);              // stat increase on levelup

        currentHP = stats.getStats(statModifiers)["hp"];        // change current hp to new full hp
        currentMP = stats.getStats(statModifiers)["mp"];
        if (currentExp >= expToNextLevel)       // check if current experience points is enough to level up
        {
            levelUp();
        }
    }

    public string getCharClass()
    {
        return charclass.ToString();
    }

    public void chooseCharClass(string value)
    {
        charclass = CharacterClass.getCharClass(value);
        Debug.Log("Character class is " + charclass.ToString());

        // init
        currentLevel = 1;
        currentExp = 0;
        expToNextLevel = charclass.getExpToNextLevel(currentLevel);
        stats = charclass.startStats;
        currentHP = stats.getStats(statModifiers)["hp"];
        currentMP = stats.getStats(statModifiers)["mp"];
    }
}
