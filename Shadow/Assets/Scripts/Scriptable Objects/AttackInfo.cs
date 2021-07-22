using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Template for storing information for the different types of attacks.
 */
[CreateAssetMenu(fileName = "New AttackInfo", menuName = "AttackInfo")]
public class AttackInfo : ScriptableObject
{
    public int baseDamage;                  // The base damage of the attack
    public int attackMultiplier;            // The attack multiplier
    public bool isPhysical;                 // Whether the damage type is physical or magical

    /**
     * Method to calculate damage done to the target by an attacker.
     * Returns the damage done.
     */
    public int CalculateDamage(Creature attacker, Creature target)
    {
        int attackStat;
        int defenseStat;

        if (isPhysical)
        {
            attackStat = attacker.getStats()["atk"];
            defenseStat = target.getStats()["def"];
        }
        else
        {
            attackStat = attacker.getStats()["matk"];
            defenseStat = target.getStats()["mdef"];
        }

        // for every level lower/higher, the attack deals 1% less/more damage to the target.
        // Capped at 10 levels lower than target and 5 levels higher than target.
        Debug.Log("Target vs Attack Level: " + target.currentLevel + " vs " + attacker.currentLevel);
        int levelDiff = Mathf.Max(Mathf.Min((target.currentLevel - attacker.currentLevel), 10), -5);
        float levelDiffMultiplier = 1f - levelDiff * 0.02f;
        
        return Mathf.FloorToInt((baseDamage + attackStat * attackMultiplier / 100f) * levelDiffMultiplier
            / (1f + 0.002f * defenseStat)) + Random.Range(0, 5);
    }
}
