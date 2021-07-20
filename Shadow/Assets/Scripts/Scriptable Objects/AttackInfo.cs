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
        
        return Mathf.FloorToInt((baseDamage + attackStat * attackMultiplier / 100f)
            / (1f + 0.002f * defenseStat));
    }
}
